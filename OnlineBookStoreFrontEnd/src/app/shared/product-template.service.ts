import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ProductTemplate } from './models/product-template.model';

@Injectable({
  providedIn: 'root'
})
export class ProductTemplateService {
  readonly BaseURI = 'https://localhost:44373/api';
  product_template_list_one: ProductTemplate[];
  product_template_list_two: ProductTemplate[];
  product_template_list_three: ProductTemplate[];
  product_template_list_four: ProductTemplate[];
  product_template_list_five: ProductTemplate[];
  resTest: string = "";

  constructor(private _http: HttpClient) { }

  getBengaliNovelList() {
    var productTemplParam = new HttpParams().set('product_category', 'Bengali Novel');
    this._http.get(this.BaseURI + '/Product/ProductTemplateList', { params: productTemplParam })
      .toPromise()
      .then(res => this.product_template_list_one = res as ProductTemplate[]);
  }

  getEnglishLiteratureList() {
    var productTemplParam = new HttpParams().set('product_category', 'English Literature');
    this._http.get(this.BaseURI + '/Product/ProductTemplateList', { params: productTemplParam })
      .toPromise()
      .then(res => this.product_template_list_two = res as ProductTemplate[]);
  }

  getBengaliPoetryList() {
    var productTemplParam = new HttpParams().set('product_category', 'Bengali Poetry');
    this._http.get(this.BaseURI + '/Product/ProductTemplateList', { params: productTemplParam })
      .toPromise()
      .then(res => this.product_template_list_three = res as ProductTemplate[]);
  }

  getTravelList() {
    var productTemplParam = new HttpParams().set('product_category', 'Travel');
    this._http.get(this.BaseURI + '/Product/ProductTemplateList', { params: productTemplParam })
      .toPromise()
      .then(res => this.product_template_list_four = res as ProductTemplate[]);
  }

  getScienceFictionList() {
    var productTemplParam = new HttpParams().set('product_category', 'Science Fiction');
    this._http.get(this.BaseURI + '/Product/ProductTemplateList', { params: productTemplParam })
      .toPromise()
      .then(res => this.product_template_list_five = res as ProductTemplate[]);
  }

}
