import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  readonly BaseURI = 'https://localhost:44373/api';

  constructor(private _http: HttpClient) { }

  generateToken() {
    return this._http.get(this.BaseURI + '/Payment/GenerateToken');
  }
}
