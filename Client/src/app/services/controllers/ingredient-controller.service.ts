import { Observable } from 'rxjs';
import { eControllerType } from 'src/app/_notgenerated/enums';
import { ApiService } from '../api.service';
import { IIngredientDto, ICommandInfo } from 'src/app/_generated/interfaces';
import { Injectable } from '@angular/core';
import * as g from '../../_notgenerated/globals';

@Injectable()
export class IngredientController extends ApiService {
    
    executeCommands(commands: ICommandInfo<IIngredientDto>[]): Observable<ICommandInfo<IIngredientDto>[]> {
        return this.executeCommandsBase<IIngredientDto>(commands, eControllerType.Ingredient);
    }

    getAllIngredients(): Observable<IIngredientDto[]> {
        const url = g.ingredientController + 'all';
        return this.http.get<IIngredientDto[]>(url);
    }

    
}