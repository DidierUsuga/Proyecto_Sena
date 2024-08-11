import { Component, OnInit, Inject } from '@angular/core';

import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Compra } from 'src/app/Interfaces/compra';
import { DetalleCompra } from 'src/app/Interfaces/detalle-compra';

@Component({
  selector: 'app-modal-detalle-compra',
  templateUrl: './modal-detalle-compra.component.html',
  styleUrls: ['./modal-detalle-compra.component.css']
})
export class ModalDetalleCompraComponent implements OnInit {

  fechaRegistro:string = "";
  numeroDocumento:string = "";
  tipoPago: string = "";
  total: string = "";
  detalleCompra: DetalleCompra[] = [];
  columnasTabla :string[] = ['producto','cantidad','precio','total']

  constructor(  @Inject(MAT_DIALOG_DATA) public _compra: Compra) { 

    this.fechaRegistro = _compra.fechaRegistro!;
    this.numeroDocumento = _compra.numeroDocumento!;
    this.tipoPago = _compra.tipoPago;
    this.total = _compra.totalTexto;
    this.detalleCompra = _compra.detalleCompra; 

  }

  ngOnInit(): void {
  }

}
