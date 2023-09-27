import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WeatherService  {
  private apiUrl = 'https://localhost:44364/weatherforecast';

  constructor(private http: HttpClient) {}

  getWeather(city: string): Observable<any> {
    const token = localStorage.getItem('AccessToken');
    let headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    const getWeatherUrl = `${this.apiUrl}/search?city=${city}`;
    return this.http.get(getWeatherUrl,{headers});
  }
}
