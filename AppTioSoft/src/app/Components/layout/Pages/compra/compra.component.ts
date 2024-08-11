import { Component, OnInit } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';

import { ProductoService } from 'src/app/Services/producto.service';
import { CompraService } from 'src/app/Services/compra.service';
import { UtilidadService } from 'src/app/Reutilizable/utilidad.service';

import { Producto } from 'src/app/Interfaces/producto';
import { Compra } from 'src/app/Interfaces/compra';
import { DetalleCompra } from 'src/app/Interfaces/detalle-compra';

import Swal from 'sweetalert2';

@Component({
  selector: 'app-compra',
  templateUrl: './compra.component.html',
  styleUrls: ['./compra.component.css'],
})
export class CompraComponent implements OnInit {
  listaProductos: Producto[] = [];
  listaProductosFiltro: Producto[] = [];

  listaProductosParaCompra: DetalleCompra[] = [];
  bloquearBotonRegistrar: boolean = false;

  productoSeleccionado!: Producto;
  tipodePagoPorDefecto: string = 'Efectivo';
  totalPagar: number = 0;

  formularioProductoCompra: FormGroup;
  columnasTabla: string[] = [
    'producto',
    'cantidad',
    'precio',
    'total',
    'accion',
  ];
  datosDetalleCompra = new MatTableDataSource(this.listaProductosParaCompra);

  retornarProductosPorFiltro(busqueda: any): Producto[] {
    const valorBuscado =
      typeof busqueda === 'string'
        ? busqueda.toLocaleLowerCase()
        : busqueda.nombre.toLocaleLowerCase();

    return this.listaProductos.filter((item) =>
      item.nombre.toLocaleLowerCase().includes(valorBuscado)
    );
  }

  constructor(
    private fb: FormBuilder,
    private _productoServicio: ProductoService,
    private _compraServicio: CompraService,
    private _utilidadServicio: UtilidadService
  ) {
    this.formularioProductoCompra = this.fb.group({
      producto: ['', Validators.required],
      cantidad: ['', Validators.required],
      precio: [''],
    });

    this._productoServicio.lista().subscribe({
      next: (data) => {
        if (data.status) {
          const lista = data.value as Producto[];
          this.listaProductos = lista.filter(
            (p) => p.esActivo == 1 && p.stock >= 0
          );
        }
      },
      error: (e) => {},
    });

    this.formularioProductoCompra
      .get('producto')
      ?.valueChanges.subscribe((value) => {
        this.listaProductosFiltro = this.retornarProductosPorFiltro(value);
      });
  }

  ngOnInit(): void {}

  mostrarProducto(producto: Producto): string {
    return producto.nombre;
  }

  productoParaCompra(event: any) {
    this.productoSeleccionado = event.option.value;
  }

  agregarProductoParaCompra() {
    const _cantidad: number = this.formularioProductoCompra.value.cantidad;
    const _precio: number =
      parseFloat(this.formularioProductoCompra.value.precio) ||
      parseFloat(this.productoSeleccionado.precio); // Use manually entered price or default price
    const _total: number = _cantidad * _precio;
    this.totalPagar = this.totalPagar + _total;

    this.listaProductosParaCompra.push({
      idProducto: this.productoSeleccionado.idProducto,
      descripcionProducto: this.productoSeleccionado.nombre,
      cantidad: _cantidad,
      precioTexto: String(_precio.toFixed(2)),
      totalTexto: String(_total.toFixed(2)),
    });

    this.datosDetalleCompra = new MatTableDataSource(
      this.listaProductosParaCompra
    );

    this.formularioProductoCompra.patchValue({
      producto: '',
      cantidad: '',
      precio: '',
    });
  }

  eliminarProducto(detalle: DetalleCompra) {
    (this.totalPagar = this.totalPagar - parseFloat(detalle.totalTexto)),
      (this.listaProductosParaCompra = this.listaProductosParaCompra.filter(
        (p) => p.idProducto != detalle.idProducto
      ));

    this.datosDetalleCompra = new MatTableDataSource(
      this.listaProductosParaCompra
    );
  }

  registrarCompra() {
    if (this.listaProductosParaCompra.length > 0) {
      this.bloquearBotonRegistrar = true;

      const request: Compra = {
        tipoPago: this.tipodePagoPorDefecto,
        totalTexto: String(this.totalPagar.toFixed(2)),
        detalleCompra: this.listaProductosParaCompra,
      };

      this._compraServicio.registrar(request).subscribe({
        next: (response) => {
          if (response.status) {
            this.totalPagar = 0.0;
            this.listaProductosParaCompra = [];
            this.datosDetalleCompra = new MatTableDataSource(
              this.listaProductosParaCompra
            );

            Swal.fire({
              icon: 'success',
              title: 'Compra Registrada!',
              text: `Numero de Compra: ${response.value.numeroDocumento}`,
            });
          } else
            this._utilidadServicio.mostrarAlerta(
              'No se pudo registrar la compra',
              'Oops'
            );
        },
        complete: () => {
          this.bloquearBotonRegistrar = false;
        },
        error: (e) => {},
      });
    }
  }
}
