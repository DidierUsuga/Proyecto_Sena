import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

import { MAT_DATE_FORMATS } from '@angular/material/core';
import * as moment from 'moment';

import { ModalDetalleCompraComponent } from '../../Modales/modal-detalle-compra/modal-detalle-compra.component';

import { Compra } from 'src/app/Interfaces/compra';
import { CompraService } from 'src/app/Services/compra.service';
import { UtilidadService } from 'src/app/Reutilizable/utilidad.service';

export const MY_DATA_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-historial-compra',
  templateUrl: './historial-compra.component.html',
  styleUrls: ['./historial-compra.component.css'],
  providers: [{ provide: MAT_DATE_FORMATS, useValue: MY_DATA_FORMATS }],
})
export class HistorialCompraComponent implements OnInit, AfterViewInit {
  formularioBusqueda: FormGroup;
  opcionesBusqueda: any[] = [
    { value: 'fecha', descripcion: 'Por fechas' },
    { value: 'numero', descripcion: 'Numero compra' },
  ];
  columnasTabla: string[] = [
    'fechaRegistro',
    'numeroDocumento',
    'tipoPago',
    'total',
    'accion',
  ];
  dataInicio: Compra[] = [];
  datosListaCompra = new MatTableDataSource(this.dataInicio);
  @ViewChild(MatPaginator) paginacionTabla!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private _compraServicio: CompraService,
    private _utilidadServicio: UtilidadService
  ) {
    this.formularioBusqueda = this.fb.group({
      buscarPor: ['fecha'],
      numero: [''],
      fechaInicio: [''],
      fechaFin: [''],
    });

    this.formularioBusqueda
      .get('buscarPor')
      ?.valueChanges.subscribe((value) => {
        this.formularioBusqueda.patchValue({
          numero: '',
          fechaInicio: '',
          fechaFin: '',
        });
      });
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.datosListaCompra.paginator = this.paginacionTabla;
  }

  aplicarFiltroTabla(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.datosListaCompra.filter = filterValue.trim().toLocaleLowerCase();
  }

  buscarCompras() {
    let _fechaInicio: string = '';
    let _fechaFin: string = '';

    if (this.formularioBusqueda.value.buscarPor === 'fecha') {
      _fechaInicio = moment(this.formularioBusqueda.value.fechaInicio).format(
        'DD/MM/YYYY'
      );
      _fechaFin = moment(this.formularioBusqueda.value.fechaFin).format(
        'DD/MM/YYYY'
      );

      if (_fechaInicio === 'Invalid date' || _fechaFin === 'Invalid date') {
        this._utilidadServicio.mostrarAlerta(
          'Debe ingresar ambas fechas',
          'Oops!'
        );
        return;
      }
    }

    this._compraServicio
      .historial(
        this.formularioBusqueda.value.buscarPor,
        this.formularioBusqueda.value.numero,
        _fechaInicio,
        _fechaFin
      )
      .subscribe({
        next: (data) => {
          if (data.status) this.datosListaCompra = data.value;
          else
            this._utilidadServicio.mostrarAlerta(
              'No se encontraron datos',
              'Oops!'
            );
        },
        error: (e) => {},
      });
  }

  verDetalleCompra(_compra: Compra) {
    this.dialog.open(ModalDetalleCompraComponent, {
      data: _compra,
      disableClose: true,
      width: '700px',
    });
  }
}
