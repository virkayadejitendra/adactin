import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class OccupationService {
  
  private url = environment.apiUrl+'Occupation';
  constructor(private httpClient: HttpClient) { }

  getOccupations(){
    return this.httpClient.get(this.url);
  }
}
