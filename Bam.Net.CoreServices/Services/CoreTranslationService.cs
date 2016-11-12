﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using Bam.Net.Translation;

namespace Bam.Net.CoreServices
{
    [Proxy("translationSvc")]
    public class CoreTranslationService : ProxyableService, IDetectLanguage, ITranslationProvider
    {
        public CoreTranslationService(
            IRepository genericRepo, 
            DaoRepository daoRepo, 
            AppConf appConf,
            IDetectLanguage languageDetector,
            ITranslationProvider translationProvider) : base(genericRepo, daoRepo, appConf)
        {
            LanguageDetector = languageDetector;
            TranslationProvider = translationProvider;
        }

        public IDetectLanguage LanguageDetector { get; set; }
        public ITranslationProvider TranslationProvider { get; set; }

        public override object Clone()
        {
            CoreTranslationService clone = new CoreTranslationService(Repository, DaoRepository, AppConf, LanguageDetector, TranslationProvider);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public Language DetectLanguage(string text)
        {
            return LanguageDetector.DetectLanguage(text);
        }

        public string Translate(string input, string languageIdentifier)
        {
            return LanguageDetector.Translate(input, languageIdentifier);
        }

        public string Translate(Language from, Language to, string input)
        {
            return TranslationProvider.Translate(from, to, input);
        }

        public string Translate(string uuidFrom, string uuidTo, string input)
        {
            return TranslationProvider.Translate(uuidFrom, uuidTo, input);
        }

        public string Translate(long languageIdFrom, long languageIdTo, string input)
        {
            return TranslationProvider.Translate(languageIdFrom, languageIdTo, input);
        }
    }
}