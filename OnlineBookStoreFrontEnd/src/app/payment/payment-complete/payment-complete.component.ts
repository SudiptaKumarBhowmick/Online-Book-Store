import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-payment-complete',
  templateUrl: './payment-complete.component.html',
  styles: []
})
export class PaymentCompleteComponent implements OnInit {
  TotalAmountPaid: number;
  NumOfProducts: number;

  constructor(private _router: Router, private _actRoute: ActivatedRoute) { }

  ngOnInit() {
    this._actRoute.queryParams.subscribe(params => {
      this.TotalAmountPaid = params['amount'],
        this.NumOfProducts = params['product']
    });
    setTimeout(() => {
      this._router.navigate(['/store']);
    }, 5000)
  };

}
