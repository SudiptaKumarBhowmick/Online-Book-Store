import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: [
  ]
})
export class HomeComponent implements OnInit {
  userDetails;

  constructor(private _router: Router, private _service: UserService, private _formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  onLogout() {
    if (localStorage.getItem('token') != null) {
      this._service.logOutEmailPassword();
    }
    else if (localStorage.getItem('app_token') != null) {
      this._service.logOutSocial();
    }
  }

}
