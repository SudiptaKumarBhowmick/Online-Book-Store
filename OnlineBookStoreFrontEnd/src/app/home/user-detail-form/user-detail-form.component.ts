import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { FormGroup, FormBuilder, Validators, NgForm, FormControl } from '@angular/forms';
import { UserService } from '../../shared/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-user-detail-form',
  templateUrl: './user-detail-form.component.html',
  styles: [
  ]
})
export class UserDetailFormComponent implements OnInit {
  
  pipe = new DatePipe('en-US');

  constructor(private _formBuilder: FormBuilder, private _service: UserService,
    private _toastr: ToastrService, private _router: Router,
    private _http: HttpClient)
  {

  }

  ngOnInit(): void {
    var user = localStorage.getItem('user');
    var splitUserToken = user.split(".");
    this._service.UserDetails(splitUserToken[0]).subscribe(
      res => {
        this.formHomeModel.controls["Email"].setValue(res[0]['email']);
        this.formHomeModel.controls["PhoneNumber"].setValue(res[0]['phoneNumber']);
      }
    )
  }

  formHomeModel = this._formBuilder.group({
    FirstName: ['', Validators.required],
    LastName: ['', Validators.required],
    Email: ['', [Validators.required, Validators.email]],
    PhoneNumber: ['', [Validators.required, Validators.minLength(11)]],
    DateOfBirth: ['', Validators.required],
    Address: ['', Validators.required],
    City: ['', Validators.required],
    Country: ['', Validators.required],
    UserTypeName: ['', Validators.required]
  });

  SaveUserDetails(UserDetailsForm: NgForm) {
    if (this.formHomeModel.invalid) {
      this.formHomeModel.markAllAsTouched();
    }
    else {
      var userStorage = localStorage.getItem('user');
      var date_of_birth = this.pipe.transform(UserDetailsForm.value.DateOfBirth, 'dd/MM/yyyy');
      this._service.saveUserDetails(UserDetailsForm, userStorage, date_of_birth).subscribe(
        (res: any) => {
          this.formHomeModel.reset();
          this._router.navigateByUrl('/store');
        },
        err => {
          this._toastr.error('User Details Saved Failed', 'Failed');
        }
      );
    }
  }

}
