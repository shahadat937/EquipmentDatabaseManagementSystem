import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../core/service/auth.service';
import { Location } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private router: Router,
    private authService: AuthService,
    private location: Location) { }

  formatDateTime(date: any): string {

    let validDate: Date;
    if (date instanceof Date) {
      validDate = date;
    } 
    else if (typeof date === 'string') {

      validDate = new Date(date);

      if (isNaN(validDate.getTime())) {
        validDate = new Date(date.replace('T', ' '));
      }
    } 

  
    const year = validDate.getFullYear();
    const month = (validDate.getMonth() + 1).toString().padStart(2, '0'); //  2-digit month
    const day = validDate.getDate().toString().padStart(2, '0');           //  2-digit day
    const hours = validDate.getHours().toString().padStart(2, '0');        //  2-digit hours
    const minutes = validDate.getMinutes().toString().padStart(2, '0');    //  2-digit minutes
    const seconds = validDate.getSeconds().toString().padStart(2, '0');    //  2-digit seconds

    // Return the formatted date string
    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
  }
}
