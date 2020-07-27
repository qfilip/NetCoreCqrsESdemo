import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { ICocktailDto, ICommandInfo } from 'src/app/_generated/interfaces';
import * as g from '../../_notgenerated/globals';
import { Observable } from 'rxjs';
import { eControllerType } from 'src/app/_notgenerated/enums';

@Injectable()
export class CocktailController extends ApiService {
    
    executeCommands(commands: ICommandInfo<ICocktailDto>[]): Observable<ICommandInfo<ICocktailDto>[]> {
        return this.executeCommandsBase<ICocktailDto>(commands, eControllerType.Cocktail);
    }

    getAllCocktails(): Observable<ICocktailDto[]> {
        const url = g.ingredientController + 'all';
        return this.http.get<ICocktailDto[]>(url);
    }
}