import { Component, OnInit } from '@angular/core';
import { UserService } from '../../shared/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: [
  ]
})
export class RegistrationComponent implements OnInit {

  constructor(public _service: UserService, private _toastr: ToastrService, private _router: Router) { }

  ngOnInit(): void {
    this._service.formModel.reset();
  }

  onSubmit() {
    if (!this._service.formModel.valid) {
      this._service.formModel.markAllAsTouched();
    }
    else {
      this._service.register().subscribe(
        (res: any) => {
          if (res.succeeded) {
            this._service.formModel.reset();
            this._router.navigateByUrl('/user/login');
          } else {
            res.errors.forEach(element => {
              switch (element.code) {
                case 'DuplicateUserName':
                  this._toastr.error('Username is already taken', 'Registration Failed!');
                  break;
                default:
                  this._toastr.error(element.description, 'Registration Failed!');
                  break;
              }
            });
          }
        },
        err => {
          console.log(err);
        }
      );
    }
  }

}
