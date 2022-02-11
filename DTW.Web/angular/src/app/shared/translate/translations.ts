
import { InjectionToken } from '@angular/core';
// import translations
 import {  LANG_EN_NAME, LANG_EN_TRANS } from './lang-en';
 import {  LANG_MK_NAME, LANG_MK_TRANS } from './lang-mk';

// import { LANG_FR_TRANS } from './lang-fr';

// translation token
// export const TRANSLATIONS = new InjectionToken('translations');
export const TRANSLATIONS = new InjectionToken('translations');
// all translations
export const dictionary = {
    // 'en': {},
    // 'mk': {}
    [LANG_EN_NAME]: LANG_EN_TRANS,
    [LANG_MK_NAME]: LANG_MK_TRANS,
  
};

// providers
export const TRANSLATION_PROVIDERS = [
    { provide: TRANSLATIONS, useValue: dictionary },
];