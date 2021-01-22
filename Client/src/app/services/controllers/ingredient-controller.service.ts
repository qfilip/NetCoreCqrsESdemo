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
        let url = g.ingredientController + g.common_executeCommands;
        return this.http.post<ICommandInfo<IIngredientDto>[]>(url, commands);
    }

    getAllIngredients(): Observable<IIngredientDto[]> {
        const url = g.ingredientController + g.common_getAll;
        return this.http.get<IIngredientDto[]>(url);
    }

    getIngredientsByIds(ingredientIds: string[]): Observable<IIngredientDto[]> {
        const url = g.ingredientController + g.common_getAllWithIds;
        return this.http.post<IIngredientDto[]>(url, ingredientIds);
    }
}