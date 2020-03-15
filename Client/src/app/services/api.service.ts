import { Injectable } from '@angular/core';
import * as g from '../_notgenerated/globals';
import { HttpClient } from '@angular/common/http';
import { ICocktailDto, IAppEventDto } from '../_generated/interfaces';
import { Observable } from 'rxjs';

@Injectable({providedIn: 'root'})
export class ApiService {
    constructor(private http: HttpClient) {}

    // cocktails
    getAllCocktails(): Observable<ICocktailDto[]> {
        const url = g.cocktailController + g.all;
        return this.http.get<ICocktailDto[]>(url);
    }

    createCocktail(cocktailDto: ICocktailDto): Observable<ICocktailDto> {
        const url = g.cocktailController + g.create;
        return this.http.post<ICocktailDto>(url, cocktailDto);
    }

    deleteCocktail(cocktailDto: ICocktailDto): Observable<ICocktailDto> {
        const url = g.cocktailController + g.remove;
        return this.http.post<ICocktailDto>(url, cocktailDto);
    }

    
    //events
    getAllEvents(): Observable<IAppEventDto[]> {
        const url = g.eventController + g.all;
        return this.http.get<IAppEventDto[]>(url);
    }
}