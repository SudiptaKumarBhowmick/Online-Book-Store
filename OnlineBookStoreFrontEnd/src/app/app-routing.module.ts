import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SocialLoginModule, AuthServiceConfig, AuthService } from 'angularx-social-login';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angularx-social-login';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './auth/auth.guard';
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

let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider("87756983518-drmdc4bqvmhnh7gqv2qos4smtgao0926.apps.googleusercontent.com")
  },
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider("395646001801854")
  }
]);
export function provideConfig() {
  return config;
}

const routes: Routes = [
  { path: '', redirectTo: '/user/login', pathMatch: 'full' },
  {
    path: 'user', component: UserComponent,
    children: [
      {
        path: 'registration', component: RegistrationComponent
      },
      {
        path: 'login', component: LoginComponent
      }
    ]
  },
  {
    path: 'home', component: HomeComponent, canActivate: [AuthGuard],
    children: [
      { path: 'user-detail-form', component: UserDetailFormComponent }
    ]
  },
  { path: 'store', component: StoreComponent },
  { path: 'all-book-list', component: AllProductListComponent },
  { path: 'cartlist', component: CartlistComponent },
  { path: 'product-details', component: ProductDetailsComponent },
  { path: 'book', component: SingleProductDetailsComponent },
  { path: 'shipment-details', component: ShipmentComponent, canActivate: [AuthGuard] },
  { path: 'payment-options', component: PaymentComponent, canActivate: [AuthGuard] },
  { path: 'payment-complete', component: PaymentCompleteComponent, canActivate: [AuthGuard] },
  { path: 'payment-detail-form', component: PaymentDetailFormComponent },
  { path: 'product-category', component: ProductCategoryComponent, canActivate: [AuthGuard] },
  { path: 'product-in-stock', component: ProductInStockComponent, canActivate: [AuthGuard] },
  { path: 'product-description', component: ProductDescriptionComponent, canActivate: [AuthGuard] },
  { path: 'user-profile', component: UserProfileComponent, canActivate: [AuthGuard] },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'product', component: ProductComponent },
  { path: 'single-product', component: SingleproductComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
    SocialLoginModule.initialize(config)],
  providers: [
    {
      provide: AuthServiceConfig,
      useFactory: provideConfig
    }
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
