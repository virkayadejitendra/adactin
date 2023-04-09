import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { PremiumData } from '../_models/PremiumData';
import { PremiumResult } from '../_models/PremiumResult';


@Injectable({
  providedIn: 'root'
})
export class PremiumService {

  private url = environment.apiUrl+'Premium';
  constructor(private httpClient: HttpClient) { }

  calculatePremium( premiumData :PremiumData )  {
    return this.httpClient.post<PremiumResult>(this.url,premiumData);
  }
}
