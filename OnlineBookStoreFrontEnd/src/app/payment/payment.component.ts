import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../shared/payment.service';
import { ToastrService } from 'ngx-toastr';
import { AppSettings } from '../app.settings';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styles: [
  ]
})
export class PaymentComponent implements OnInit {
  paymentResponse: any;
  chargeAmount = localStorage.getItem('TotalAmount');
  NumOfProducts = localStorage.getItem('addToCartCount');

  constructor(private _paymentService: PaymentService, private _toastr: ToastrService,
    private _router: Router) { }

  ngOnInit(): void {
  }

  clientTokenURL = AppSettings.GET_CLIENT_TOKEN_URL;
  createPurchaseURL = AppSettings.CREATE_PURCHASE_URL;
  

  onDropinLoaded(event) {
    console.log("dropin loaded...");
  }

  onPaymentStatus(response): void {
    this.paymentResponse = response;
    if (response == 'Succeded') {
      this._router.navigate(['/payment-complete'], { queryParams: { payment: 'OnPaymentSuccess', amount: this.chargeAmount, product: this.NumOfProducts } });
      localStorage.removeItem('TotalAmount');
      localStorage.removeItem('productCode');
      localStorage.setItem('addToCartCount', '0');
    }
    else {
      this._toastr.error("Payment Failed", "Failed");
      window.location.reload();
    }
  }

}
