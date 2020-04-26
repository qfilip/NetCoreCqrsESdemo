import { eCommand } from '../_generated/enums';

export class PageLoaderInfo {
    loading: boolean;
    message: string;
}

export class Command<TParam> {
    parameters: any;
    execute: (parameter: TParam) => any;
    commandType: eCommand;
}