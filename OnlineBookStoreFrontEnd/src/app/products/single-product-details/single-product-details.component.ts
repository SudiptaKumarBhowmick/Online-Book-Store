import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../shared/product.service';
import { ToastrService } from 'ngx-toastr';
import { SingleProductDetails } from '../../shared/models/single-product-details.model';
import { ProductTemplateService } from '../../shared/product-template.service';

@Component({
  selector: 'app-single-product-details',
  templateUrl: './single-product-details.component.html',
  styles: [
  ]
})
export class SingleProductDetailsComponent implements OnInit {
  ProductCode: string;
  addToCartCount: number = 0;

  incrementCartCount: number = 0;
  BookTitle: string;
  ProductDetailsAuthorName: string;
  CategoryName: string;
  Ratings: number;
  ProductPrice: number;
  InStockAvailable: string;
  ProductImageSource: string;

  ProductSummary: string;
  Title: string;
  Publisher: string;
  Edition: string;
  NumOfPages: number;
  Country: string;
  Language: string;
  AuthorImage: string;
  SpecificationSummaryAuthorName: string;
  AuthorDescription: string;

  LogInBtn: string;

  constructor(private _actroute: ActivatedRoute,
    public _productService: ProductService,
    private _toastr: ToastrService,
    private _router: Router) { }

  ngOnInit(): void {
    this._actroute.queryParams.subscribe(params => {
      this.ProductCode = params['code'];
    });

    this.addToCartCount = Number(localStorage.getItem("addToCartCount"));

    this._productService.singleProductDetails(this.ProductCode).subscribe(
      res => {
        this.BookTitle = res[0].bookTitle;
        this.ProductDetailsAuthorName = res[0].authorName;
        this.CategoryName = res[0].categoryName;
        this.Ratings = res[0].ratings;
        this.ProductPrice = res[0].productPrice;
        this.InStockAvailable = res[0].inStockAvailable;
        this.ProductImageSource = res[0].productImage;
      },
      err => {
        this._toastr.error('Refresh the page again', 'Error Occured');
      }
    );

    this._productService.singleProductSpecificationSummary(this.ProductCode).subscribe(
      res => {
        this.ProductSummary = res[0].productSummary;
        this.Title = res[0].title;
        this.Publisher = res[0].publisher;
        this.Edition = res[0].edition;
        this.NumOfPages = res[0].numOfPages;
        this.Country = res[0].country;
        this.Language = res[0].language;
        this.AuthorImage = res[0].authorImage;
        this.SpecificationSummaryAuthorName = res[0].authorName;
        this.AuthorDescription = res[0].authorDescription;
      },
      err => {
        this._toastr.error('Refresh the page again', 'Error Occured');
      }
    );

    if (localStorage.getItem("token") === null) {
      this.LogInBtn = "LogIn";
    }
    else {
      this.LogInBtn = "LogOut";
    }

  }

  AddToCart() {
    let cartCount: string[];
    let code: string[];

    if (localStorage.getItem("addToCartCount") === null) {
      this.addToCartCount += 1;
      //cartCount = [this.addToCartCount.toString()];
      code = [this.ProductCode.toString()];
      console.log("code: " + code);

      localStorage.setItem('addToCartCount', this.addToCartCount.toString());
      localStorage.setItem('productCode', code.toString());
    }
    else {
      this.incrementCartCount = Number(localStorage.getItem('addToCartCount'));
      if (this.incrementCartCount > 0) {
        this.incrementCartCount += 1;
        this.addToCartCount = this.incrementCartCount;
        console.log("addToCartCount: " + this.addToCartCount);

        //cartCount = this.incrementCartCount.toString().split(',');
        code = localStorage.getItem('productCode').split(',');
        console.log("code: " + code);

        //cartCount.push(this.addToCartCount.toString());
        code.push(this.ProductCode.toString());
        console.log("code: " + code);

        localStorage.setItem('addToCartCount', this.incrementCartCount.toString());
        localStorage.setItem('productCode', code.toString());
      }
    }
    console.log("Final: " + localStorage.getItem("addToCartCount"));
    console.log("Final: " + localStorage.getItem("productCode"));
  }

  onClickLogOut() {
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
