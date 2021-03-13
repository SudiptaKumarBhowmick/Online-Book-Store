import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'; 
import { ProductService } from '../../shared/product.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
declare var $;

@Component({
  selector: 'app-product-category',
  templateUrl: './product-category.component.html',
  styles: []
})
export class ProductCategoryComponent implements OnInit {
  title = "Product Category";
  data: any[] = [];
  productCategoryForm: FormGroup;
  submitted = false;
  EventValue: any = "Save";
  LogInBtn: string;

  constructor(private productService: ProductService, private _toastr: ToastrService,
    private _router: Router) { }

  ngOnInit() {
    if (localStorage.getItem("token") != null || localStorage.getItem("app_token") != null) {
      console.log(1);
      this.LogInBtn = "LogOut";
    }
    else {
      this.LogInBtn = "LogIn";
    }
    this.getdata();
    this.productCategoryForm = new FormGroup({
      categoryId: new FormControl(null),
      categoryName: new FormControl("", [Validators.required]),
      categoryDescription: new FormControl("", [Validators.required])
    });
  }

  getdata() {
    this.productService.getDataProductCategory().subscribe((res: any[]) => {
      this.data = res;
    },
      err => {
        this._toastr.error('Please refresh the page', 'Error');
      });
  }

  Save() {
    this.submitted = true;

    if (this.productCategoryForm.invalid) {
      this.productCategoryForm.markAllAsTouched();
      return;
    }
    this.productService.postDataProductCategory(this.productCategoryForm.value).subscribe(res => {
      this.resetFrom();
      this._toastr.success('Product category saved succcessfully', 'Successful');
    },
      err => {
        this._toastr.error('Product category saved failed', 'Error');
      })
  }

  Update() {
    this.submitted = true;

    if (this.productCategoryForm.invalid) {
      this.productCategoryForm.markAllAsTouched();
      return;
    }
    this.productService.putDataProductCategory(this.productCategoryForm.value.categoryId, this.productCategoryForm.value).subscribe((res) => {
      this.resetFrom();
      this._toastr.success('Product category updated succcessfully', 'Successful');
    },
      err => {
        this._toastr.error('Product category updated failed', 'Error');
      })
  }

  deleteData(categoryId) {
    if (confirm('Are you sure to delete this data?')) {
      this.productService.deleteDataProductCategory(categoryId).subscribe(res => {
        this.getdata();
        this._toastr.success('Product category deleted succcessfully', 'Successful');
      },
        err => {
          this._toastr.error('Product category deleted failed', 'Error');
        })
    }
    
  }

  EditData(Data) {
    this.productCategoryForm.controls["categoryId"].setValue(Data.productCategoryId);
    this.productCategoryForm.controls["categoryName"].setValue(Data.categoryName);
    this.productCategoryForm.controls["categoryDescription"].setValue(Data.categoryDescription);
    this.EventValue = "Update";
  }

  resetFrom() {
    this.getdata();
    this.productCategoryForm.reset();
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
