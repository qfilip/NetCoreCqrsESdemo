import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { ICocktailDto, ICommandInfo } from 'src/app/_generated/interfaces';
import * as g from '../../_notgenerated/globals';
import { Observable } from 'rxjs';
import { eControllerType } from 'src/app/_notgenerated/enums';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class CocktailController {
    private apiUrl = g.cocktailController; 
    
    constructor(private http: HttpClient) {}
    
    executeCommands(commands: ICommandInfo<ICocktailDto>[]): Observable<ICommandInfo<ICocktailDto>[]> {
        let url = this.apiUrl + g.action;
        return this.http.post<ICommandInfo<ICocktailDto>[]>(url, commands);
    }

    getAllCocktails(): Observable<ICocktailDto[]> {
        const url = this.apiUrl + 'all';
        return this.http.get<ICocktailDto[]>(url);
    }

    getCocktailEditData() {
        const url = this.apiUrl + 'get-cocktail-edit-data';
    }
}