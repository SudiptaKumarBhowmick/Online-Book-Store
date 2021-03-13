import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-singleproduct',
  templateUrl: './singleproduct.component.html',
  styles: [
  ]
})
export class SingleproductComponent implements OnInit {
  statusClass = 'nav-close';

  constructor() { }

  ngOnInit(): void {
  }

  w3_open() {
    this.statusClass = 'nav-open';
  }

  w3_close() {
    this.statusClass = 'nav-close';
  }

}
