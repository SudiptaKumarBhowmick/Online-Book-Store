import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ProductService } from '../../shared/product.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-description',
  templateUrl: './product-description.component.html',
  styles: []
})
export class ProductDescriptionComponent implements OnInit {
  private encodedImage: String = "";
  private encodedImagePart = "data:image/png;base64,";
  private finalEncodedString: String = "";
  AuthorImageSrc: string;
  SelProductCatgId: string = "";
  submitted = false;
  EventValue: any = "Save";
  SelectedCategoryText;
  SelectedProductNameText;
  productDescriptionList: any[] = [];

  @ViewChild('AuthorImage', { static: false }) AuthorImage: ElementRef;

  constructor(public _service: ProductService, private _formBuilder: FormBuilder,
    public _toastr: ToastrService) { }

  ngOnInit() {
    this._service.productCategoryList();
    this.getdata();
  }

  formProductDescriptionModel = this._formBuilder.group({
    ProductDescId: new FormControl(null),
    ProductCategory: new FormControl("", [Validators.required]),
    ProductName: new FormControl("", [Validators.required]),
    AuthorName: new FormControl("", [Validators.required]),
    AuthorDescription: new FormControl("", [Validators.required]),
    AuthorImageValue: new FormControl("", [Validators.required]),
    ProductSummary: new FormControl("", [Validators.required]),
    PublisherName: new FormControl("", [Validators.required]),
    Edition: new FormControl("", [Validators.required]),
    NumofPages: new FormControl("", [Validators.required]),
    Country: new FormControl("", [Validators.required]),
    Language: new FormControl("", [Validators.required])
  });

  FillProductNameDDL($event) {
    this.SelectedCategoryText = $event.target.options[$event.target.options.selectedIndex].text;
    this._service.productNameList(this.SelProductCatgId);
  }

  GetProductNameText($event) {
    this.SelectedProductNameText = $event.target.options[$event.target.options.selectedIndex].text;
  }

  readImageFile(evt) {
    var files = evt.target.files;
    var file = files[0];

    if (files && file) {
      var reader = new FileReader();

      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt) {
    var binaryString = readerEvt.target.result;
    //this.base64textString.push('data:image/png;base64,' + btoa(binaryString));
    this.encodedImage = btoa(binaryString);
    this.finalEncodedString = this.encodedImagePart + this.encodedImage;
    this.AuthorImageSrc = this.finalEncodedString as string;
    //this.ProductImageValue = this.finalEncodedString as string;
    this.formProductDescriptionModel.controls['AuthorImageValue'].setValue(this.finalEncodedString);
  }

  Save() {
    if (this.formProductDescriptionModel.invalid) {
      this.formProductDescriptionModel.markAllAsTouched();
    }
    else {
      var product_description = {
        ProductDescId: 0,
        ProductCategory: this.SelectedCategoryText,
        ProductName: this.SelectedProductNameText,
        AuthorName: this.formProductDescriptionModel.value.AuthorName,
        AuthorDescription: this.formProductDescriptionModel.value.AuthorDescription,
        AuthorImageValue: this.formProductDescriptionModel.value.AuthorImageValue,
        ProductSummary: this.formProductDescriptionModel.value.ProductSummary,
        PublisherName: this.formProductDescriptionModel.value.PublisherName,
        Edition: this.formProductDescriptionModel.value.Edition,
        NumofPages: this.formProductDescriptionModel.value.NumofPages,
        Country: this.formProductDescriptionModel.value.Country,
        Language: this.formProductDescriptionModel.value.Language,
        ProductId: this.formProductDescriptionModel.value.ProductName
      };
      this._service.postProductDescription(product_description).subscribe(
        (res: any) => {
          this.resetFrom();
          this.AuthorImageSrc = "";
          this.formProductDescriptionModel.controls['AuthorImageValue'].setValue("");
          this.AuthorImage.nativeElement.value = null;
          this._toastr.success('Product Description Saved Successfully', 'Successful');
        },
        err => {
          this._toastr.error('Product Description Saved Failed', 'Failed');
        }
        )
    }
  }

  getdata() {
    this._service.getDataProductDescription().subscribe((res: any[]) => {
      this.productDescriptionList = res;
      console.log(res);
    },
      err => {
        this._toastr.error('Please refresh the page', 'Error');
      });
  }

  resetFrom() {
    this.getdata();
    this.formProductDescriptionModel.reset();
    this.EventValue = "Save";
    this.submitted = false;
  }

  EditData(Data) {
    this.formProductDescriptionModel.controls["ProductDescId"].setValue(Data.productDescId);
    this.formProductDescriptionModel.controls["ProductCategory"].setValue(Data.productCategoryId);
    this._service.productNameList(Data.productCategoryId);
    if (this._service.product_category_list.length > 0) {
      this.formProductDescriptionModel.controls["ProductName"].setValue(Data.productId);
    }
    this.formProductDescriptionModel.controls["AuthorName"].setValue(Data.authorName);
    this.formProductDescriptionModel.controls["AuthorDescription"].setValue(Data.authorDescription);
    this.AuthorImageSrc = Data.authorImageValue as string;
    this.formProductDescriptionModel.controls["AuthorImageValue"].setValue(Data.authorImageValue);
    this.formProductDescriptionModel.controls["ProductSummary"].setValue(Data.productSummary);
    this.formProductDescriptionModel.controls["PublisherName"].setValue(Data.publisherName);
    this.formProductDescriptionModel.controls["Edition"].setValue(Data.edition);
    this.formProductDescriptionModel.controls["NumofPages"].setValue(Data.numofPages);
    this.formProductDescriptionModel.controls["Country"].setValue(Data.country);
    this.formProductDescriptionModel.controls["Language"].setValue(Data.language);
    this.EventValue = "Update";
  }

  Update() {
    this.submitted = true;
    if (this.formProductDescriptionModel.invalid) {
      this.formProductDescriptionModel.markAllAsTouched();
      return;
    }
    this._service.putDataProductDescription(this.formProductDescriptionModel.value.ProductDescId, this.formProductDescriptionModel.value).subscribe(
      res => {
        this.resetFrom();
        this.AuthorImageSrc = "";
        this.formProductDescriptionModel.controls["AuthorImageValue"].setValue("");
        this.AuthorImage.nativeElement.value = null;
        this._toastr.success('Product description updated successfully', 'Successful');
      },
      err => {
        this._toastr.success('Product description updated failed', 'Failed');
      }
    );
  }
}
