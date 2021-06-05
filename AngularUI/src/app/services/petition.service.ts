import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PetitionService {

  readonly baseURI = 'http://localhost:55022/api/petition';

  constructor(private http: HttpClient) { }

  getList(){
    return this.http.get(this.baseURI + '/List');
  }

  getMyList(){
    return this.http.get(this.baseURI + '/MyList');
  }

  get(id: number){
    return this.http.get(this.baseURI + '/Get?id=' + id);
  }

  add(formData: any){
    var body = {
      title: formData.Title,
      description: formData.Description,
      topic: formData.Topic,
      petitionDate: formData.PetitionDate,
      country: formData.Country,
      city: formData.City,
      signatureList: formData.SignatureList
    };

    return this.http.post(this.baseURI + '/Add', body);
  }

  update(formData: any){
    var body = {
      id: formData.Id,
      title: formData.Title,
      description: formData.Description,
      topic: formData.Topic,
      petitionDate: formData.PetitionDate,
      country: formData.Country,
      city: formData.City,
      signatureList: formData.SignatureList
    };

    return this.http.put(this.baseURI + '/Update', body);
  }

  delete(id: number){
    return this.http.delete(this.baseURI + '/Delete?id=' + id);
  }
}
