import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, Validators, NgForm, FormControl } from '@angular/forms';
import { UserService } from '../../shared/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styles: [
  ]
})
export class ProductDetailsComponent implements OnInit {

  private encodedImage: String = "";
  private encodedImagePart = "data:image/png;base64,";
  private finalEncodedString: String = "";
  ProductImageSrc: string;
  readonly BaseURI = 'https://localhost:44373/api';
  LogInBtn: string;
  productDetailsList: any[] = [];
  submitted = false;
  EventValue: any = "Save";

  @ViewChild('ProductImage', { static: false}) ProductImage: ElementRef;

  constructor(private _formBuilder: FormBuilder, public _service: UserService,
    private _router: Router, private _toastr: ToastrService,
    private _http: HttpClient) { }

  ngOnInit(): void {
    if (localStorage.getItem("token") != null || localStorage.getItem("app_token") != null) {
      this.LogInBtn = "LogOut";
    }
    else {
      this.LogInBtn = "LogIn";
    }
    this._service.productCategoryList();
    this.getdata();
  }

  product_categories = [
    {
      id: '',
      name: '---Select---'
    },
    {
      id: '1',
      name: 'cat 1'
    },
    {
      id: '2',
      name: 'cat 2'
    },
    {
      id: '3',
      name: 'cat 3'
    }];

  formProductModel = this._formBuilder.group({
    ProductId: new FormControl(null),
    ProductName: new FormControl("", [Validators.required]),
    ProductPrice: new FormControl("", [Validators.required]),
    ProductImageValue: new FormControl("", [Validators.required]),
    ProductCategory: new FormControl("", [Validators.required])
  });

  Save() {
    if (this.formProductModel.invalid) {
      this.formProductModel.markAllAsTouched();
    }
    else {
      this._service.postProductDetails(this.formProductModel.value).subscribe(
        (res: any) => {
          this.resetFrom();
          this.ProductImageSrc = "";
          this.formProductModel.controls['ProductImageValue'].setValue("");
          this.ProductImage.nativeElement.value = null;
          this._toastr.success('Product Details Saved Successfully', 'Successful');
        },
        err => {
          this._toastr.error('Product Details Saved Failed', 'Failed');
        }
      );
    }
  }

  readImageFile(evt) {
    var files = evt.target.files;
    var file = files[0];

    if (files && file) {
      var reader = new FileReader();

      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt) {
    var binaryString = readerEvt.target.result;
    //this.base64textString.push('data:image/png;base64,' + btoa(binaryString));
    this.encodedImage = btoa(binaryString);
    this.finalEncodedString = this.encodedImagePart + this.encodedImage;
    this.ProductImageSrc = this.finalEncodedString as string;
    //this.ProductImageValue = this.finalEncodedString as string;
    this.formProductModel.controls['ProductImageValue'].setValue(this.finalEncodedString);
  }

  onClickLogOut() {
    if (this.LogInBtn.toString() == "LogIn") {
      this._router.navigate(['/user/login']);
    }
    if (this.LogInBtn.toString() == "LogOut") {
      localStorage.clear();
      this._router.navigate(['/user/login']);
    }
  }

  getdata() {
    this._service.getDataProductDetails().subscribe((res: any[]) => {
      this.productDetailsList = res;
    },
      err => {
        this._toastr.error('Please refresh the page', 'Error');
      });
  }

  Update() {
    this.submitted = true;

    if (this.formProductModel.invalid) {
      this.formProductModel.markAllAsTouched();
      return;
    }
    this._service.putDataProductDetails(this.formProductModel.value.ProductId, this.formProductModel.value).subscribe(
      (res) => {
        this.resetFrom();
        this.ProductImageSrc = "";
        this.formProductModel.controls['ProductImageValue'].setValue("");
        this.ProductImage.nativeElement.value = null;
        this._toastr.success('Product details updated succcessfully', 'Successful');
      },
      err => {
        this._toastr.error('Product details updated failed', 'Error');
      })
  }

  EditData(Data) {
    this.formProductModel.controls["ProductId"].setValue(Data.productId);
    this.formProductModel.controls["ProductName"].setValue(Data.productName);
    this.formProductModel.controls["ProductPrice"].setValue(Data.productPrice);
    this.ProductImageSrc = Data.productImage as string;
    this.formProductModel.controls["ProductImageValue"].setValue(Data.productImage);
    this.formProductModel.controls["ProductCategory"].setValue(Data.categoryId);
    this.EventValue = "Update";
  }

  resetFrom() {
    this.getdata();
    this.formProductModel.reset();
    this.EventValue = "Save";
    this.submitted = false;
  }

  deleteData(productId) {
    if (confirm('Are you sure to delete this data?')) {
      this._service.deleteDataProductDetails(productId).subscribe(
        res => {
          this.resetFrom();
          this._toastr.success('Product details deleted succcessfully', 'Successful');
        },
        err => {
          this._toastr.error('Product details deleted failed', 'Error');
        })
    }

  }

}
