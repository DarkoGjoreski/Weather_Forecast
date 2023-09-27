import { Component } from '@angular/core';
import { WeatherService } from '../services/weatherService.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-weater-search',
  templateUrl: './weater-search.component.html',
  styleUrls: ['./weater-search.component.css']
})

export class WeaterSearchComponent {

  jsonData: any;
  city: any;
  cityName: any;

  constructor(private weatherService: WeatherService,
    private router: Router) { }

  search(){
    debugger;
    this.weatherService.getWeather(this.cityName).subscribe((data)=>{
      this.jsonData = data;
      this.city= this.jsonData.city.name;
    })
  }

  signout(){
        localStorage.clear();
        this.router.navigate(['/home']);
  }

  update(value: string) { this.cityName = value; }

}
