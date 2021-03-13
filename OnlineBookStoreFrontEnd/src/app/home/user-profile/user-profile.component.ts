import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormControl, NgForm } from '@angular/forms';
import { UserService } from '../../shared/user.service';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from '../../shared/product.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styles: []
})
export class UserProfileComponent implements OnInit {
  birthDate: string;
  date = new FormControl(new Date());
  userStorage = localStorage.getItem('user');
  userId = this.userStorage.split('.');
  pipe = new DatePipe('en-US');
  LogInBtn: string;

  constructor(private _formBuilder: FormBuilder, private _userService: UserService,
    private _toastr: ToastrService, private _productService: ProductService) { }

  userProfileForm = this._formBuilder.group({
    FirstName: ['', Validators.required],
    LastName: ['', Validators.required],
    DateOfBirth: ['', Validators.required],
    Address: ['', Validators.required],
    City: ['', Validators.required],
    PhoneNumber: ['', [Validators.required, Validators.minLength(11)]]
  });

  ngOnInit() {
    if (localStorage.getItem("token") != null || localStorage.getItem("app_token") != null) {
      console.log(1);
      this.LogInBtn = "LogOut";
    }
    else {
      this.LogInBtn = "LogIn";
    }

    this._userService.UserProfileDetails(this.userId[0]).subscribe(
      res => {
        this.userProfileForm.controls["FirstName"].setValue(res[0]['firstName']);
        this.userProfileForm.controls["LastName"].setValue(res[0]['lastName']);

        this.birthDate = res[0]['dateOfBirth'];
        var splitDate = this.birthDate.split('/');
        this.date = new FormControl(new Date(Number(splitDate[2]), Number(splitDate[1]) - 1, Number(splitDate[0])));

        this.userProfileForm.controls["Address"].setValue(res[0]['address']);
        this.userProfileForm.controls["City"].setValue(res[0]['city']);
        this.userProfileForm.controls["PhoneNumber"].setValue(res[0]['phoneNumber']);
      },
      err => {
        this._toastr.error('Please refresh the page', 'Error');
      });
  }

  Update(userProfileDetailsForm: NgForm) {
    if (this.userProfileForm.invalid) {
      this.userProfileForm.markAllAsTouched();
      return;
    }
    var date_of_birth = this.pipe.transform(this.userProfileForm.value.DateOfBirth, 'dd/MM/yyyy');
    this._productService.putUserProfileData(this.userId[0], date_of_birth, userProfileDetailsForm).subscribe(
      (res) => {
        //this.data = data;
        this._toastr.success('Profile updated succcessfully', 'Successful');
      },
      err => {
        this._toastr.error('Profile updated failed', 'Error');
      })
  }

}
