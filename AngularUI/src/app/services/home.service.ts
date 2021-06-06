import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  public isAuthenticated: boolean = false;
  readonly baseURI = 'http://localhost:55022/api/home';

  constructor(private http: HttpClient) { }

  register(formData: any) {
    
    var body = {
      UserName: formData.UserName,
      Email: formData.Email,
      Password: formData.Passwords.Password,
      FirstName: formData.FirstName,
      LastName: formData.LastName,
      DateOfBirth: formData.DateOfBirth,
      Country: formData.Country,
      City: formData.City,
      AddressLine: formData.AddressLine,
      ZipCode: formData.ZipCode
    };

    return this.http.post(this.baseURI + '/register', body);
  }

  login(formData: any) {
    return this.http.post(this.baseURI + '/login', formData);
  }
}
