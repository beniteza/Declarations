import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PageLoadingService {

  constructor() { }

  showLoading(isActive: boolean){
    if(isActive)
      document.querySelector('.page-loading').classList.add('active');
    else{
      setTimeout(function () {
        document.querySelector('.page-loading').classList.remove('active');
      }, 500);
    }
  }
}
