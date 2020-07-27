import { Observable } from 'rxjs';
import { eControllerType } from 'src/app/_notgenerated/enums';
import { ApiService } from '../api.service';
import { IIngredientDto, ICommandInfo } from 'src/app/_generated/interfaces';
import { Injectable } from '@angular/core';
import * as g from '../../_notgenerated/globals';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class IngredientController {
    constructor(private http: HttpClient) {}
    
    executeCommands(commands: ICommandInfo<IIngredientDto>[]): Observable<ICommandInfo<IIngredientDto>[]> {
        let url = g.cocktailController + g.action;
        return this.http.post<ICommandInfo<IIngredientDto>[]>(url, commands);
    }

    getAllIngredients(): Observable<IIngredientDto[]> {
        const url = g.ingredientController + 'all';
        return this.http.get<IIngredientDto[]>(url);
    }
}