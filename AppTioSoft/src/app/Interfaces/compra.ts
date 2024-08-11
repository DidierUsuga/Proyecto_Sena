import { DetalleCompra } from "./detalle-compra"

export interface Compra {
    idCompra?:number,
    numeroDocumento?:string,
    tipoPago:string,
    fechaRegistro?:string,
    totalTexto:string,
    detalleCompra: DetalleCompra[]
}
