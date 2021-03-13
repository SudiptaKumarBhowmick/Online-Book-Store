import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../shared/product.service';
import { Validators, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-in-stock',
  templateUrl: './product-in-stock.component.html',
  styles: []
})
export class ProductInStockComponent implements OnInit {
  productInStockList: any[] = [];
  productInStockForm: FormGroup;
  submitted = false;
  EventValue: any = "Save";
  LogInBtn: string;

  constructor(private _formBuilder: FormBuilder, public _productService: ProductService,
    private _toastr: ToastrService, private _router: Router) { }

  ngOnInit() {
    if (localStorage.getItem("token") != null || localStorage.getItem("app_token") != null) {
      console.log(1);
      this.LogInBtn = "LogOut";
    }
    else {
      this.LogInBtn = "LogIn";
    }
    this._productService.productList();
    this.getdata();
    this.productInStockForm = new FormGroup({
      ProductStockId: new FormControl(null),
      ProductNameId: new FormControl("", [Validators.required]),
      ProductInStock: new FormControl("", [Validators.required]),
      ProductQuantity: new FormControl("", [Validators.required])
    });
  }

  getdata() {
    this._productService.getDataProductInStock().subscribe((res: any[]) => {
      this.productInStockList = res;
    },
      err => {
        this._toastr.error('Please refresh the page', 'Error');
      });
  }

  Save() {
    this.submitted = true;
    if (this.productInStockForm.invalid) {
      console.log(1);
      this.productInStockForm.markAllAsTouched();
      return;
    }
    this._productService.postDataProductInStock(this.productInStockForm.value).subscribe(
      res => {
      //this.data = data;
        this.resetFrom();
        this._toastr.success('Product stock details saved succcessfully', 'Successful');
    },
      err => {
        this._toastr.error('Product stock details saved failed', 'Error');
      })
  }

  Update() {
    this.submitted = true;

    if (this.productInStockForm.invalid) {
      this.productInStockForm.markAllAsTouched();
      return;
    }
    this._productService.putDataProductInStock(this.productInStockForm.value.ProductStockId, this.productInStockForm.value).subscribe(
      (res) => {
      //this.data = data;
      this.resetFrom();
        this._toastr.success('Product stock details updated succcessfully', 'Successful');
    },
      err => {
        this._toastr.error('Product stock details updated failed', 'Error');
      })
  }

  EditData(Data) {
    this.productInStockForm.controls["ProductStockId"].setValue(Data.productStockId);
    this.productInStockForm.controls["ProductNameId"].setValue(Data.productNameId);
    this.productInStockForm.controls["ProductInStock"].setValue(Data.productInStock);
    this.productInStockForm.controls["ProductQuantity"].setValue(Data.productQuantity);
    this.EventValue = "Update";
  }

  deleteData(productStockId) {
    if (confirm('Are you sure to delete this data?')) {
      this._productService.deleteDataProductInStock(productStockId).subscribe(
        res => {
        this.getdata();
          this._toastr.success('Product stock details deleted succcessfully', 'Successful');
      },
        err => {
          this._toastr.error('Product stock details deleted failed', 'Error');
        })
    }

  }

  resetFrom() {
    this.getdata();
    this.productInStockForm.reset();
    this.EventValue = "Save";
    this.submitted = false;
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
}
