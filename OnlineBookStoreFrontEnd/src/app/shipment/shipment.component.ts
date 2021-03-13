import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ShipmentService } from '../shared/shipment.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import * as CryptoJS from 'crypto-js';

@Component({
  selector: 'app-shipment',
  templateUrl: './shipment.component.html',
  styles: [
  ]
})
export class ShipmentComponent implements OnInit {
  EncOrderStatusCode: string;
  DecOrderStatusCode: string;

  LogInBtn: string;

  constructor(private _formBuilder: FormBuilder, public _shipmentService: ShipmentService,
    private _toastr: ToastrService, private _router: Router,
    private _actRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this._actRoute.queryParams.subscribe(params => {
      this.EncOrderStatusCode = params['orders']
    });
    if (localStorage.getItem("token") === null) {
      this.LogInBtn = "LogIn";
    }
    else {
      this.LogInBtn = "LogOut";
    }
  }

  onSubmit() {
    if (!this._shipmentService.formShipmentModel.valid) {
      this._shipmentService.formShipmentModel.markAllAsTouched();
    }
    else {
      this.DecOrderStatusCode = CryptoJS.AES.decrypt(this.EncOrderStatusCode.trim(), 'secret key 123').toString(CryptoJS.enc.Utf8);
      this._shipmentService.SaveShipmentDetails(this.DecOrderStatusCode).subscribe(
        (res: any) => {
          this._shipmentService.formShipmentModel.reset();
          this._router.navigateByUrl('/payment-options');
        },
        err => {
          this._toastr.error('Error', 'Shipment Details Saved Failed');
        }
      );
    }
  }

  onClickLogOut() {
    if (this.LogInBtn.toString() == "LogIn") {
      this._router.navigate(['/user/login']);
    }
    if (this.LogInBtn.toString() == "LogOut") {
      localStorage.removeItem('token');
      localStorage.removeItem('productCode');
      localStorage.removeItem('user');
      localStorage.removeItem('addToCartCount');
      this._router.navigate(['/user/login']);
    }
  }
}
