import { Component, OnInit, Inject, HostListener, Output, Input, EventEmitter, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { ProductTemplateService } from '../../shared/product-template.service';
import { Router } from '@angular/router';
import { DOCUMENT } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, of } from 'rxjs';
import { map, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { BookSearchService } from '../../shared/book-search.service';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { MediaMatcher } from '@angular/cdk/layout';

@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styles: []
})

export class StoreComponent implements OnInit {
  addToCartCount: number = 0;
  incrementCartCount: number = 0;
  windowScrolled: boolean;
  LogInBtn: string;
  UserProfileBtn: string;
  public showProfileBtn: boolean = false;
  booksArray: any[] = [];
  readonly BaseURI = "https://localhost:44373/api";
  public clients: Observable<any[]>;
  private searchTerms = new Subject<string>();
  public ClientName = '';
  public flag: boolean = true;
  getCurrentYear: number = (new Date()).getFullYear();
  statusClass = 'nav-close';

  constructor(public _productTempService: ProductTemplateService, private _router: Router,
    @Inject(DOCUMENT) private document: Document, private _http: HttpClient,
    private _bookSearchService: BookSearchService, private _formBuilder: FormBuilder)
  {}

  ngOnInit(): void {
    this._productTempService.getBengaliNovelList();
    this._productTempService.getEnglishLiteratureList();
    this._productTempService.getBengaliPoetryList();
    this._productTempService.getTravelList();
    this._productTempService.getScienceFictionList();
    this.addToCartCount = Number(localStorage.getItem("addToCartCount"));
    if (localStorage.getItem("token") === null) {
      this.showProfileBtn = false;
      this.LogInBtn = "LogIn";
    }
    else {
      this.showProfileBtn = true;
      this.UserProfileBtn = "User Profile"
      this.LogInBtn = "LogOut";
    }

    //this.clients = this.searchTerms.pipe(
    //  debounceTime(300) ,       // wait for 300ms pause in events  
    //  distinctUntilChanged(),   // ignore if next search term is same as previous  
    //  switchMap((term: string) => term   // switch to new observable each time  
    //    // return the http search observable  
    //    ? this._bookSearchService.search(term)
    //    // or the observable of empty heroes if no search term  
    //    : Observable.of<any[]>([]))
    //  .catch(error => {
    //    // TODO: real error handling  
    //    console.log(error);
    //    return Observable.of<any[]>([]);
    //  })); 
  }

  leftbar_open() {
    this.statusClass = 'nav-open';
  }

  leftbar_close() {
    this.statusClass = 'nav-close';
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
    this._router.navigate(['/book/'], { queryParams: { code: product_code } });
  }

  @HostListener("window:scroll", [])
  onWindowScroll() {
    if (window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop > 100) {
      this.windowScrolled = true;
    }
    else if (this.windowScrolled && window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop < 10) {
      this.windowScrolled = false;
    }
  }

  scrollToTop() {
    (function smoothscroll() {
      var currentScroll = document.documentElement.scrollTop || document.body.scrollTop;
      if (currentScroll > 0) {
        window.requestAnimationFrame(smoothscroll);
        window.scrollTo(0, currentScroll - (currentScroll / 8));
      }
    })();
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
        //this._router.navigate(['/store']);
        window.location.reload();
    }
  }

  AllBookList(product_category) {
    this._router.navigate(['/all-book-list'], { queryParams: { category: product_category } });
  }
}
