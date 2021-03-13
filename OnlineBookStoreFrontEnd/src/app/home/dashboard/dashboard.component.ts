import { Component, OnInit } from '@angular/core';
import { ProductTemplateService } from '../../shared/product-template.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styles: [
  ]
})
export class DashboardComponent implements OnInit {
  addToCartCount: number = 0;
  incrementCartCount: number = 0;

  constructor(public _productTempService: ProductTemplateService, private _router: Router) { }

  ngOnInit(): void {
    this._productTempService.getBengaliNovelList();
    this._productTempService.getEnglishLiteratureList();
    this._productTempService.getBengaliPoetryList();
    this.addToCartCount = Number(localStorage.getItem("addToCartCount"));
  }

  AddToCart(product_code) {
    let cartCount: string[];
    let code: string[];

    if (localStorage.getItem("addToCartCount") === null) {
      this.addToCartCount += 1;
      //cartCount = [this.addToCartCount.toString()];
      code = [product_code.toString()];
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
        code.push(product_code.toString());
        console.log("code: " + code);

        localStorage.setItem('addToCartCount', this.incrementCartCount.toString());
        localStorage.setItem('productCode', code.toString());
      }
    }
    console.log("Final: " + localStorage.getItem("addToCartCount"));
    console.log("Final: " + localStorage.getItem("productCode"));
  }

  ViewDetails(product_code) {
    this._router.navigate(['/book'], { queryParams: { code: product_code } });
  }
}
