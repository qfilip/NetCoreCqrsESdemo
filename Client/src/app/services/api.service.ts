import { Injectable } from '@angular/core';
import * as g from '../_notgenerated/globals';
import { HttpClient } from '@angular/common/http';
import { ICocktailDto } from '../_generated/interfaces';
import { Observable } from 'rxjs';

@Injectable({providedIn: 'root'})
export class ApiService {
    constructor(private http: HttpClient) {}

    getAllCocktails(): Observable<ICocktailDto[]> {
        const url = g.apiUrl + g.cocktailController + g.allCocktails;
        return this.http.get<ICocktailDto[]>(url);
    }

    createCocktail(cocktailDto: ICocktailDto): Observable<ICocktailDto> {
        const url = g.apiUrl + g.cocktailController + g.createCocktail;
        console.log(url);
        return this.http.post<ICocktailDto>(url, cocktailDto);
    }
}