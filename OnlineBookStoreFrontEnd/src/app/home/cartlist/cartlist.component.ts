import { Component, OnInit } from '@angular/core';
import { CartlistService } from '../../shared/cartlist.service';
import { ToastrService } from 'ngx-toastr';
import { CartProductList } from '../../shared/models/cart-product-list.model';
import { Router, ActivatedRoute } from '@angular/router';
import * as CryptoJS from 'crypto-js';

@Component({
  selector: 'app-cartlist',
  templateUrl: './cartlist.component.html',
  styles: [
  ]
})
export class CartlistComponent implements OnInit {
  cart_product_details_list: CartProductList[];
  TotalPrice: number = 0;
  shippingPrice: number = 50;
  payableTotal: number = 0;
  totalNumofItems: number = 0;
  conversionEncryptOutput: string;
  orderStatusCode: string;

  LogInBtn: string;
  ChckOutBtn: string =  '1';

  constructor(private _cartList: CartlistService, private toastr: ToastrService,
    private _router: Router) {
}

  ngOnInit(): void {
    this._cartList.cartListProductDetails().subscribe(
      res => {
        this.cart_product_details_list = res as CartProductList[];
        this.sumPrice(this.cart_product_details_list);
        this.totalQuantity(this.cart_product_details_list);
      },
      err => {
        this.toastr.error("Please try again", "Error Occurred");
      }
    );
    if (localStorage.getItem("token") === null) {
      this.LogInBtn = "LogIn";
      this.ChckOutBtn = '0';
    }
    else {
      this.LogInBtn = "LogOut";
    }
  }

  sumPrice(cartproductlist: CartProductList[]) {
    for (let i = 0; i < cartproductlist.length; i++) {
      this.TotalPrice = this.TotalPrice + cartproductlist[i]["productPrice"];
    }
    this.sumTotal();
  }

  sumTotal() {
    this.payableTotal = this.TotalPrice + this.shippingPrice;
  }

  totalQuantity(cartproductlist: CartProductList[]) {
    for (let i = 0; i < cartproductlist.length; i++) {
      this.totalNumofItems = this.totalNumofItems + cartproductlist[i]["productQuantity"];
    }
  }

  ProceedToCheckOut() {
    this._cartList.cartListCheckOut(this.cart_product_details_list, this.payableTotal).subscribe(
      (res: any) => {
        localStorage.setItem('TotalAmount', this.payableTotal.toString());
        this.orderStatusCode = res.toString();
        this.conversionEncryptOutput = CryptoJS.AES.encrypt(this.orderStatusCode.trim(), 'secret key 123').toString();
        //this._router.navigateByUrl('/shipment-details');
        this._router.navigate(['/shipment-details'], { queryParams: { orders: this.conversionEncryptOutput} })
      },
      err => {
        this.toastr.error("Error Occur", "Error Function");
      }
    );
  }

  onClickLogOutBtn() {
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
