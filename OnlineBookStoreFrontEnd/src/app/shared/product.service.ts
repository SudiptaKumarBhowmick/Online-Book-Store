import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { SingleProductDetails } from './models/single-product-details.model';
import { Product } from './models/product.model';
import { AllBookList } from './models/all-book-list.model';
import { ProductCategory } from './models/product-category.model';


@Injectable({
  providedIn: 'root'
})
export class ProductService {
  readonly BASE_URI = 'https://localhost:44373/api';
  single_product_details_list: SingleProductDetails[];
  product_list: Product[];
  product_name_list: Product[];
  all_book_list: AllBookList[];
  product_category_list: ProductCategory[];

  constructor(private _http: HttpClient) { }

  singleProductDetails(code) {
    var singleProDtlParam = new HttpParams().set('code', code);
    return this._http.get(this.BASE_URI + '/Product/SingleProductDetails', { params: singleProDtlParam });
  }

  singleProductSpecificationSummary(code) {
    var singleProDtlParam = new HttpParams().set('code', code);
    return this._http.get(this.BASE_URI + '/Product/SingleProductSpecificationSummary', { params: singleProDtlParam });
  }

  //product category
  getDataProductCategory() {
    return this._http.get(this.BASE_URI + '/ProductCategory/ProductCategoryListData');
  }

  postDataProductCategory(formData) {
    return this._http.post(this.BASE_URI + '/ProductCategory/SaveProductCategroy', formData);
  }

  putDataProductCategory(categoryId, formData) {
    return this._http.put(this.BASE_URI + '/ProductCategory/' + categoryId, formData);
  }

  deleteDataProductCategory(categoryId) {
    return this._http.delete(this.BASE_URI + '/ProductCategory/' + categoryId);
  } 

  //product in stock
  productList() {
    this._http.get(this.BASE_URI + '/ProductInStock/ProductList')
      .toPromise()
      .then(res => { this.product_list = res as Product[] });
  }

  getDataProductInStock() {
    return this._http.get(this.BASE_URI + '/ProductInStock/ProductInStockListData');
  }

  postDataProductInStock(formData) {
    return this._http.post(this.BASE_URI + '/ProductInStock/SaveProductStockDetails', formData);
  }

  putDataProductInStock(productStockId, formData) {
    return this._http.put(this.BASE_URI + '/ProductInStock/' + productStockId, formData);
  }

  deleteDataProductInStock(productStockId) {
    return this._http.delete(this.BASE_URI + '/ProductInStock/' + productStockId);
  }

  //user profile
  putUserProfileData(userId, birthDate, formData) {
    var userProfileDetails = {
      FirstName: formData.value.FirstName,
      LastName: formData.value.LastName,
      DateOfBirth: birthDate,
      Address: formData.value.Address,
      City: formData.value.City,
      PhoneNumber: formData.value.PhoneNumber
    };
    return this._http.put(this.BASE_URI + '/UserProfile/' + userId, userProfileDetails);
  }

  //all product list
  getAllBookList(product_category) {
    var allBookListParam = new HttpParams().set('product_category', product_category);
    return this._http.get(this.BASE_URI + '/Product/AllBookList', { params: allBookListParam })
      .toPromise()
      .then(res => this.all_book_list = res as AllBookList[]);
  }

  addToWishList(formData) {
    console.log(formData);
    return this._http.post(this.BASE_URI + '/Product/TestPurpose', formData);
  }

  //product description
  productCategoryList() {
    this._http.get(this.BASE_URI + '/ProductDescription/ProductCategoryList')
      .toPromise()
      .then(res => {
        this.product_category_list = res as ProductCategory[]
      });
  }

  productNameList(productCatgId) {
    var productCategoryId = new HttpParams().set('productCategoryId', productCatgId);
    this._http.get(this.BASE_URI + '/ProductDescription/ProductList', { params: productCategoryId })
      .toPromise()
      .then(res => { this.product_name_list = res as Product[] });
  }

  getDataProductDescription() {
    return this._http.get(this.BASE_URI + '/ProductDescription/ProductDescriptionListData');
  }

  postProductDescription(ProductDescriptionEntryForm) {
    return this._http.post(this.BASE_URI + '/ProductDescription/SaveProductDescription', ProductDescriptionEntryForm);
  }

  putDataProductDescription(productDescId, formData) {
    return this._http.put(this.BASE_URI + '/ProductDescription/' + productDescId, formData);
  }

}
