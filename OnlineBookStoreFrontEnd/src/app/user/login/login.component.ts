import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { UserService } from '../../shared/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AuthService } from 'angularx-social-login';
import { FacebookLoginProvider, GoogleLoginProvider } from 'angularx-social-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
  ]
})
export class LoginComponent implements OnInit {
  readonly BaseURI = "https://localhost:44373/api";
  userData: any[] = [];
  logInStorageEmail: string;

  constructor(private _service: UserService, private _router: Router, private _toastr: ToastrService,
    private _formBuilder: FormBuilder, private _http: HttpClient,
    private _authService: AuthService) { }

  //formModelLogIn = {
  //  UserName: '',
  //  Password: ''
  //}

  //formModelLogIn: FormGroup;
  isSubmitted = false;

  formModelLogIn = this._formBuilder.group({
    Email: ['', Validators.required],
    Password: ['', [Validators.required, Validators.minLength(6)]]
  });

  get formControls() {
    return this.formModelLogIn.controls;
  }

  ngOnInit(): void {
    if (localStorage.getItem('token') != null) {
      var user = localStorage.getItem('user');
      var splitUserToken = user.split(".");
      this._service.checkUserStatus(splitUserToken[0]).subscribe(
        (res: any) => {
          if (res.userStatus == 1) {
            this._router.navigateByUrl('/home');
          }
          else {
            this._router.navigateByUrl('/store');
          }
        })
    }
  }

  TestToastr() {
    this._toastr.success('Success', 'Test Successful');
  }

  login(form: NgForm) {
    this.isSubmitted = true;
    if (this.formModelLogIn.invalid) {
      return;
    }
    else {
      this._service.login(form.value).subscribe(
        (res: any) => {
          localStorage.setItem('token', res.token);
          var formBody = {
            Email: form.control.get('Email').value,
            Password: ""
          };
          this._http.post(this.BaseURI + '/ApplicationUser/UserStorage', form.value).subscribe(
            (res: any) => {
              localStorage.setItem('user', res.storage);
              var splitResponse = res.storage.split(".");
              this._service.checkUserStatus(splitResponse[0]).subscribe(
                (res: any) => {
                  if (res.userStatus == 1) {
                    this._router.navigateByUrl('/home');
                  }
                  else {
                    this._router.navigateByUrl('/store');
                  }
                }
              )
            },
            err => {
              if (err.status == 400) {
                this._toastr.error('Incorrect username or password', 'Authentication Failed');
              }
              else
                console.log(err);
            }
          );
        },
        err => {
          if (err.status == 400) {
            this._toastr.error('LogIn Failed', 'Authentication Failed');
          }
          else
            console.log(err);
        }
      );
    }
  }

  logInWithGoogle(platform: string): void {
    platform = GoogleLoginProvider.PROVIDER_ID;

    this._authService.signIn(platform).then(
      (res) => {
        this.userData.push({
          UserId: res.id,
          Provider: res.provider,
          FirstName: res.firstName,
          LastName: res.lastName,
          Email: res.email
        });
        this.logInStorageEmail = res.email;
        localStorage.setItem('app_token', res.idToken);
        this._service.socialLogin(this.userData[0]).subscribe(
          (res: any) => {
            var formBody = {
              Email: this.logInStorageEmail,
              Password: ""
            };
            this._http.post(this.BaseURI + '/ApplicationUser/UserStorage', formBody).subscribe(
              (res: any) => {
                localStorage.setItem('user', res.storage);
                this._router.navigateByUrl('/home');
              },
              err => {
                this._toastr.error("LogIn Failed", "Authentication Failed");
              }
            );
          },
          err => {
            if (err.status == 400) {
              this._toastr.error('LogIn Failed', 'Authentication Failed');
            }
            else
              console.log(err);
          }
        );
      },
      (err) => {
        this._toastr.error('Username or password is incorrect', 'Authentication Failed');
      }
    )
  }

  logInWithFacebook(platform: string): void {
    platform = FacebookLoginProvider.PROVIDER_ID;

    this._authService.signIn(platform).then(
      (res) => {
        this.userData.push({
          UserId: res.id,
          Provider: res.provider,
          FirstName: res.firstName,
          LastName: res.lastName,
          Email: res.email
        });
        this.logInStorageEmail = res.email;
        this._service.socialLogin(this.userData[0]).subscribe(
          (res: any) => {
            localStorage.setItem('app_token', res.authToken);
            var formBody = {
              Email: this.logInStorageEmail,
              Password: ""
            };
            this._http.post(this.BaseURI + '/ApplicationUser/UserStorage', formBody).subscribe(
              (res: any) => {
                localStorage.setItem('user', res.storage);
                this._router.navigateByUrl('/home');
              },
              err => {
                this._toastr.error("LogIn Failed", "Authentication Failed");
              }
            );
          },
          err => {
            if (err.status == 400) {
              this._toastr.error('LogIn Failed', 'Authentication Failed');
            }
            else
              console.log(err);
          }
        );
      },
      (err) => {
        this._toastr.error('Username or password is incorrect', 'Authentication Failed');
      }
    )
  }

}
