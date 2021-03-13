import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CartProductList } from './models/cart-product-list.model';

@Injectable({
  providedIn: 'root'
})
export class CartlistService {
  readonly BASE_URI = 'https://localhost:44373/api';
  orderDetailsList = [];

  constructor(private _http: HttpClient) { }

  cartListProductDetails() {
    var codeList = localStorage.getItem('productCode');
    var codeListParam = new HttpParams().set('codeList', codeList);
    return this._http.get(this.BASE_URI + '/Product/CartListProductDetails', { params: codeListParam });
  }

  cartListCheckOut(cartList: CartProductList[], TotalAmount) {
    var OrderDetails = {};
    var _userStorage = localStorage.getItem('user').split('.');
    for (var i = 0; i < cartList.length; i++) {
      OrderDetails = {
        ProductCode: cartList[i].productCode.toString(),
        ProductTitle: cartList[i].productTitle.toString(),
        ProductPrice: cartList[i].productPrice.toString(),
        ProductQuantity: cartList[i].productQuantity.toString(),
        TotalAmount: TotalAmount.toString(),
        UserId: _userStorage[0].toString()
      };
      this.orderDetailsList.push(OrderDetails);
    }
    return this._http.post(this.BASE_URI + '/Product/ProceedToCheckOut', this.orderDetailsList);
  }
}
