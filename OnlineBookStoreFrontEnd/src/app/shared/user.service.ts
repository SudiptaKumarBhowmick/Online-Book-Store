import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ProductCategory } from '../shared/models/product-category.model';
import { Router } from '@angular/router';
import { AuthService } from 'angularx-social-login';
import { ProductDetailsList } from './models/product-details-list.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  readonly BaseURI = 'https://localhost:44373/api';
  product_category_list: ProductCategory[];
  product_details_list: ProductDetailsList[];
  endPoint: string; 

  constructor(private _formBuilder: FormBuilder, private _http: HttpClient,
    private _router: Router, private _authService: AuthService) { }

  formModel = this._formBuilder.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.required, Validators.email]],
    PhoneNumber: ['', [Validators.required, Validators.minLength(11)]],
    FullName: ['',Validators.required],
    Passwords: this._formBuilder.group({
      Password: ['', [Validators.required, Validators.minLength(6)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comaprePasswords })
  });

  comaprePasswords(_formGroup: FormGroup) {
    let confirmPassCntrl = _formGroup.get('ConfirmPassword');
    if (confirmPassCntrl.errors == null || 'passwordMismatch' in confirmPassCntrl.errors) {
      if (_formGroup.get('Password').value != confirmPassCntrl.value) {
        confirmPassCntrl.setErrors({ passwordMismatch: true });
      }
      else {
        confirmPassCntrl.setErrors(null);
      }
    }
  }

  register() {
    var body = {
      FullName: this.formModel.value.FullName,
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      PhoneNumber: this.formModel.value.PhoneNumber,
      Password: this.formModel.value.Passwords.Password
    };
    return this._http.post(this.BaseURI + '/ApplicationUser/Register', body);
  }

  login(formData) {
    return this._http.post(this.BaseURI + '/ApplicationUser/LogIn', formData);
  }

  socialLogin(userData) {
    return this._http.post(this.BaseURI + '/ApplicationUser/SocialAuthentication', userData);
  }

  getUserProfile() {
    var tokenHeader = new HttpHeaders({ 'Authorization':'Bearer ' + localStorage.getItem('token') });
    return this._http.get(this.BaseURI + '/UserProfile', { headers: tokenHeader });
  }

  saveUserDetails(UserDetailsFormData, userStorage, date_of_birth) {
    console.log(date_of_birth);
    var _userStorage = userStorage.split('.');
    //var dateofbirth = this.datepipe.transform(UserDetailsFormData.value.DateOfBirth);
    var userDetails = {
      FirstName: UserDetailsFormData.value.FirstName,
      LastName: UserDetailsFormData.value.LastName,
      Email: UserDetailsFormData.value.Email,
      PhoneNumber: UserDetailsFormData.value.PhoneNumber,
      dateOfBirth: date_of_birth,
      Address: UserDetailsFormData.value.Address,
      City: UserDetailsFormData.value.City,
      Country: UserDetailsFormData.value.Country,
      UserTypeName: UserDetailsFormData.value.UserTypeName,
      userId: _userStorage[0],
      userName: _userStorage[1]
    };
    return this._http.post(this.BaseURI + '/Home/UserDetails', userDetails);
  }

  savePaymentDetails(PaymentDetailsFormData, userStorage) {
    var _userStorage = userStorage.split('.');
    var paymentDetails = {
      FullName: PaymentDetailsFormData.value.FullName,
      Name: PaymentDetailsFormData.value.Name,
      PhoneNumber: PaymentDetailsFormData.value.PhoneNumber,
      CardNumber: PaymentDetailsFormData.value.CardNumber,
      ExpirationDate: PaymentDetailsFormData.value.Month + '/' + PaymentDetailsFormData.value.Year,
      AddressLineOne: PaymentDetailsFormData.value.AddressLineOne,
      AddressLineTwo: PaymentDetailsFormData.value.AddressLineTwo,
      City: PaymentDetailsFormData.value.City,
      State: PaymentDetailsFormData.value.State,
      Country: PaymentDetailsFormData.value.Country,
      Zip: PaymentDetailsFormData.value.Zip,
      userId: _userStorage[0]
    };
    return this._http.post(this.BaseURI + '/Home/PaymentDetails', paymentDetails);
  }

  productCategoryList() {
    this._http.get(this.BaseURI + '/Product/ProductCategoryList')
      .toPromise()
      .then(res => {
        this.product_category_list = res as ProductCategory[]
      });
  }

  logOutSocial() {
    localStorage.removeItem('app_token');
    localStorage.removeItem('productCode');
    localStorage.removeItem('user');
    localStorage.removeItem('addToCartCount');
    this._authService.signOut();
    this._router.navigate(['/user/login']);
  }

  logOutEmailPassword() {
    localStorage.removeItem('token');
    localStorage.removeItem('productCode');
    localStorage.removeItem('user');
    localStorage.removeItem('addToCartCount');
    this._router.navigate(['/user/login']);
  }

  //user profile
  UserProfileDetails(user_id) {
    var userId = new HttpParams().set('userId', user_id);
    return this._http.get(this.BaseURI + '/UserProfile/UserProfileDetails', { params: userId });
  }

  //home
  UserDetails(user_id) {
    var userId = new HttpParams().set('userId', user_id);
    return this._http.get(this.BaseURI + '/UserProfile/UserDetails', { params: userId });
  }

  //Product Details
  getDataProductDetails() {
    return this._http.get(this.BaseURI + '/Product/ProductDetailsList');
  }

  postProductDetails(ProductDetailsEntryForm) {
    //var productDetails = {
    //  ProductName: ProductDetailsEntryForm.value.ProductName,
    //  ProductPrice: ProductDetailsEntryForm.value.ProductPrice,
    //  ProductImage: ProductDetailsEntryForm.value.ProductImageValue,
    //  ProductCategoryId: ProductDetailsEntryForm.value.ProductCategory
    //};
    return this._http.post(this.BaseURI + '/Product/ProductDetails', ProductDetailsEntryForm);
    //return this._http.post(this.BaseURI + '/Product/ProductDetailsInfo', productDetails);
  }

  putDataProductDetails(productId, formData) {
    return this._http.put(this.BaseURI + '/Product/' + productId, formData);
  }

  deleteDataProductDetails(productId) {
    return this._http.delete(this.BaseURI + '/Product/' + productId);
  }

  //login
  checkUserStatus(user_id) {
    var userId = new HttpParams().set('userId', user_id);
    return this._http.get(this.BaseURI + '/ApplicationUser/CheckUserStatus', { params: userId });
  }
}
