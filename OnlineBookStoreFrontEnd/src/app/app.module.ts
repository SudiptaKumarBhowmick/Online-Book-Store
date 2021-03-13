import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MediaMatcher } from '@angular/cdk/layout';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './angular-material/angular-material.module';
import { ToastrModule } from 'ngx-toastr';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule, NativeDateModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { NgxBraintreeModule } from 'ngx-braintree';
import { DataTablesModule } from 'angular-datatables';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { JwPaginationModule } from 'jw-angular-pagination';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserService } from './shared/user.service';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { UserDetailFormComponent } from './home/user-detail-form/user-detail-form.component';
import { PaymentDetailFormComponent } from './home/payment-detail-form/payment-detail-form.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { ProductComponent } from './home/product/product.component';
import { SingleproductComponent } from './home/singleproduct/singleproduct.component';
import { CartlistComponent } from './home/cartlist/cartlist.component';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { SingleProductDetailsComponent } from './products/single-product-details/single-product-details.component';
import { ShipmentComponent } from './shipment/shipment.component';
import { PaymentComponent } from './payment/payment.component';
import { PaymentCompleteComponent } from './payment/payment-complete/payment-complete.component';
import { StoreComponent } from './home/store/store.component';
import { ProductCategoryComponent } from './products/product-category/product-category.component';
import { ProductInStockComponent } from './products/product-in-stock/product-in-stock.component';
import { UserProfileComponent } from './home/user-profile/user-profile.component';
import { AllProductListComponent } from './products/all-product-list/all-product-list.component';
import { ProductDescriptionComponent } from './products/product-description/product-description.component';

const materialModules = [
  MatTableModule,
  MatPaginatorModule,
  MatSortModule
];

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    RegistrationComponent,
    LoginComponent,
    HomeComponent,
    UserDetailFormComponent,
    PaymentDetailFormComponent,
    DashboardComponent,
    ProductComponent,
    SingleproductComponent,
    CartlistComponent,
    ProductDetailsComponent,
    SingleProductDetailsComponent,
    ShipmentComponent,
    PaymentComponent,
    PaymentCompleteComponent,
    StoreComponent,
    ProductCategoryComponent,
    ProductInStockComponent,
    UserProfileComponent,
    AllProductListComponent,
    ProductDescriptionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    ToastrModule.forRoot(),
    FormsModule,
    MatNativeDateModule,
    MatDatepickerModule,
    NativeDateModule,
    MatInputModule,
    NgxBraintreeModule,
    DataTablesModule,
    materialModules,
    Ng2SearchPipeModule,
    JwPaginationModule,
    NgxPaginationModule
  ],
  exports: [
    materialModules
  ],
  providers: [UserService, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }, MediaMatcher],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
