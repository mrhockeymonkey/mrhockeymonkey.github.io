// this service demonstrates some possible ways of typing responses
// from backend services. 

// personally I prefer deserializing...

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, tap } from 'rxjs/operators';

export interface IWeatherForcast {
  Date: string;
  Summary: string;
  SomethingElse: string; // this doesnt actually exist
}

export interface Desirializable {
  desirialize(input: any): this;
}

export class WeatherForecast implements Desirializable {
  Date: string;
  Summary: string;

  desirialize(input: any): this {
    Object.assign(this, input);
    // this.CustomType = new CustomType().deserialize(input.CustomType)
    return this;
  }
}

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  readonly url: string = "https://localhost:44308/weatherforecast";

  constructor(private httpClient: HttpClient) { }

  getWeatherVanilla() {
    console.log("called getWeatherSimple()");
    return this.httpClient.get(this.url);
  }

  // Type checking is compile-time only so there is no garunteed the returned data will
  // match that of your interface. 
  // This is lacking the ability to check the actual data is correct!?
  getWeatherWeaklyTyped() {
    console.log("called getWeatherTyped()");
    this.httpClient
      .get<IWeatherForcast>(this.url) // this lets us access properies in tap like below "SomethingElse"
      .pipe(
        tap(val => console.log(val.Date)),
        tap(val => console.log(val.SomethingElse)) // this never existed but was part of interface
      )
      .subscribe(
        (data) => {
          console.log(typeof(data)) // object
          console.log(data) // no garuntee this data matches interface
        }
      );
  }

  // to work with proper types, deserialize responses
  getWeatherStronglyTyped() {
    console.log("called getWeatherStronglyTyped()")
    this.httpClient
      .get(this.url)
      .pipe(
        tap(val => {
          const istyped = val instanceof WeatherForecast; 
          console.log("IsTyped: " + istyped)}),
        map(val => new WeatherForecast().desirialize(val)),
        tap(val => {
          const istyped = val instanceof WeatherForecast; 
          console.log("IsTyped: " + istyped)})
      )
      .subscribe(
        (data) => {
          console.log(data) // we are now working with a concrete class
        }
      )
  }
}
