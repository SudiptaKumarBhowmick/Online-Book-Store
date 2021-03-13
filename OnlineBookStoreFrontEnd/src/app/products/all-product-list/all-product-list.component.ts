import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../shared/product.service';
import { AllBookList } from '../../shared/models/all-book-list.model';

@Component({
  selector: 'app-all-product-list',
  templateUrl: './all-product-list.component.html',
  styles: []
})
export class AllProductListComponent implements OnInit {
  product_category: string;
  addToCartCount: number = 0;
  incrementCartCount: number = 0;
  LogInBtn: string;
  statusClass = 'nav-close';
  filterTerm: string;
  items = [];
  pageOfItems: Array<any>;
  p: number = 1;

  constructor(private _actroute: ActivatedRoute, private _productService: ProductService,
    private _router: Router) { }

  ngOnInit() {
    this._actroute.queryParams.subscribe(params => {
      this.product_category = params['category'];
    });
    this._productService.getAllBookList(this.product_category);
    this.addToCartCount = Number(localStorage.getItem("addToCartCount"));
    if (localStorage.getItem("token") === null) {
      this.LogInBtn = "LogIn";
    }
    else {
      this.LogInBtn = "LogOut";
    }
  }

  displayedColumns: string[] = ['Product Code', 'Product Image', 'Product Name', 'Product Description', 'Author Name', 'Product Price'];
  dataSource = new MatTableDataSource<AllBookList>(this._productService.all_book_list);
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  ViewDetails(product_code) {
    this._router.navigate(['/book/'], { queryParams: { code: product_code } });
  }

  AddToCart(product_code) {
    let cartCount: string[];
    let code: string[];

    if (localStorage.getItem("addToCartCount") === null) {
      this.addToCartCount += 1;
      //cartCount = [this.addToCartCount.toString()];
      code = [product_code.toString()];

      localStorage.setItem('addToCartCount', this.addToCartCount.toString());
      localStorage.setItem('productCode', code.toString());
    }
    else {
      this.incrementCartCount = Number(localStorage.getItem('addToCartCount'));
      if (this.incrementCartCount > 0) {
        this.incrementCartCount += 1;
        this.addToCartCount = this.incrementCartCount;

        //cartCount = this.incrementCartCount.toString().split(',');
        code = localStorage.getItem('productCode').split(',');

        //cartCount.push(this.addToCartCount.toString());
        code.push(product_code.toString());

        localStorage.setItem('addToCartCount', this.incrementCartCount.toString());
        localStorage.setItem('productCode', code.toString());
      }
    }
  }

  AddToFavorite(product_code) {
    var user = localStorage.getItem('user');
    var splitUserToken = user.split(".");
    console.log('Function Called 1');
    var formData = {
      UserId: splitUserToken[0],
      ProductCode: product_code
    };
    this._productService.addToWishList(formData);
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

  leftbar_open() {
    this.statusClass = 'nav-open';
  }

  leftbar_close() {
    this.statusClass = 'nav-close';
  }

  onChangePage(pageOfItems: Array<any>) {
    // update current page of items
    this.pageOfItems = pageOfItems;
  }
}

/* Static data */

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H' },
  { position: 2, name: 'Helium', weight: 4.0026, symbol: 'He' },
  { position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li' },
  { position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be' },
  { position: 5, name: 'Boron', weight: 10.811, symbol: 'B' },
  { position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C' },
  { position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N' },
  { position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O' },
  { position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F' },
  { position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne' },
];
