import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, NgForm } from '@angular/forms';
import { UserService } from '../../shared/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-payment-detail-form',
  templateUrl: './payment-detail-form.component.html',
  styles: [
  ]
})
export class PaymentDetailFormComponent implements OnInit {

  constructor(private _formBuilder: FormBuilder, private _service: UserService,
    private _router: Router, private _toastr: ToastrService) { }

  ngOnInit(): void {
  }

  formPaymentDetailModel = this._formBuilder.group({
    FullName: ['', Validators.required],
    Name: [''],
    PhoneNumber: ['', [Validators.required, Validators.minLength(11)]],
    CardNumber: ['', [Validators.required, Validators.minLength(20)]],
    Month: ['', Validators.required],
    Year: ['', Validators.required],
    AddressLineOne: ['', Validators.required],
    AddressLineTwo: ['', Validators.required],
    City: ['', Validators.required],
    State: ['', Validators.required],
    Country: ['', Validators.required],
    Zip: ['', Validators.required],
  });

  SavePaymentDetails(PaymentDetailsForm: NgForm)
  {
    if (this.formPaymentDetailModel.invalid) {
      this.formPaymentDetailModel.markAllAsTouched();
    }
    else {
      var userStorage = localStorage.getItem('user');
      this._service.savePaymentDetails(PaymentDetailsForm, userStorage).subscribe(
        (res: any) => {
          this.formPaymentDetailModel.reset();
          this._router.navigateByUrl('/dashboard');
        },
        err => {
          this._toastr.error('Payment Details Saved Failed', 'Failed');
        }
      );
    }
  }

}
