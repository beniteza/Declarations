import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-page-loading',
  templateUrl: './page-loading.component.html',
  styleUrls: ['./page-loading.component.css']
})
export class PageLoadingComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    let preloader = document.querySelector('.page-loading');
    // preloader.classList.remove('active');
    setTimeout(function () {
      preloader.remove();
    }, 1000);
  }
}
