import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  readonly baseURI = 'http://localhost:55022/api/account';

  constructor(private http: HttpClient) { }

  getAccountInfo() {
    return this.http.get(this.baseURI + '/accountinfo');
  }

  updateAccount(formData: any){
    return this.http.put(this.baseURI + '/accountinfo', formData);
  }

  changePassword(formData: any){
    return this.http.post(this.baseURI + '/changepassword', formData);
  }
}
