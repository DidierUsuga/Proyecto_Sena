import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../Interfaces/response-api';
import { Compra } from '../Interfaces/compra';


@Injectable({
  providedIn: 'root'
})
export class CompraService {
  private urlApi:string = environment.endpoint + "Compra/";

  constructor(private http:HttpClient) { }

  registrar(request: Compra):Observable<ResponseApi>{
    return this.http.post<ResponseApi>(`${this.urlApi}Registrar`,request)
  }

  historial(buscarPor:string,numeroCompra:string,fechaInicio:string,fechaFin:string):Observable<ResponseApi>{
    return this.http.get<ResponseApi>(`${this.urlApi}Historial?buscarPor=${buscarPor}&numeroCompra=${numeroCompra}&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
  }

}
