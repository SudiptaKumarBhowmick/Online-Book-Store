import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ShipmentService {
  readonly BaseURI = 'https://localhost:44373/api';

  constructor(private _formBuilder: FormBuilder, private _http: HttpClient) { }

  formShipmentModel = this._formBuilder.group({
    FullName: ['', Validators.required],
    AddressLineOne: ['', Validators.required],
    AddressLineTwo: ['', Validators.required],
    City: ['', Validators.required],
    StateProvinceRegion: ['', Validators.required],
    Zip: ['', Validators.required],
    Country: ['', Validators.required],
    ShipmentDate: ['', Validators.required]
  });

  SaveShipmentDetails(orderStatusCode) {
    var userStorage = localStorage.getItem('user').split('.');
    var shipmentDetails = {
      FullName: this.formShipmentModel.value.FullName,
      AddressLineOne: this.formShipmentModel.value.AddressLineOne,
      AddressLineTwo: this.formShipmentModel.value.AddressLineTwo,
      City: this.formShipmentModel.value.City,
      StateProvinceRegion: this.formShipmentModel.value.StateProvinceRegion,
      Zip: this.formShipmentModel.value.Zip,
      Country: this.formShipmentModel.value.Country,
      ShipmentDate: this.formShipmentModel.value.ShipmentDate,
      userId: userStorage[0],
      OrderStatusCode: orderStatusCode
    };
    console.log(shipmentDetails);
    return this._http.post(this.BaseURI + '/Shipment/SaveShipmentDetails', shipmentDetails);
  }
}
