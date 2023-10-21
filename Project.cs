namespace DictionariesApp
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, Dictionary<string, List<string>>> dictionaries = new Dictionary<string, Dictionary<string, List<string>>>();

            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Создать словарь");
                Console.WriteLine("2. Добавить слово и его перевод");
                Console.WriteLine("3. Заменить слово или его перевод");
                Console.WriteLine("4. Удалить слово или перевод");
                Console.WriteLine("5. Искать перевод слова");
                Console.WriteLine("6. Экспортировать словарь");
                Console.WriteLine("7. Выход");

                Console.Write("Выберите пункт меню: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateDictionary(dictionaries);
                        break;
                    case "2":
                        AddWordTranslation(dictionaries);
                        break;
                    case "3":
                        ReplaceWordTranslation(dictionaries);
                        break;
                    case "4":
                        RemoveWordTranslation(dictionaries);
                        break;
                    case "5":
                        SearchWordTranslation(dictionaries);
                        break;
                    case "6":
                        ExportDictionary(dictionaries);
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void CreateDictionary(Dictionary<string, Dictionary<string, List<string>>> dictionaries)
        {
            Console.Write("Введите название словаря: ");
            string dictionaryName = Console.ReadLine();

            if (dictionaries.ContainsKey(dictionaryName))
            {
                Console.WriteLine("Словарь с таким названием уже существует.");
                return;
            }

            Console.WriteLine("Введите тип словаря (например, англо-русский): ");
            string dictionaryType = Console.ReadLine();

            dictionaries.Add(dictionaryName, new Dictionary<string, List<string>>());
            Console.WriteLine($"Словарь '{dictionaryName}' ({dictionaryType}) успешно создан.");
        }

        static void AddWordTranslation(Dictionary<string, Dictionary<string, List<string>>> dictionaries)
        {
            Console.Write("Введите название словаря: ");
            string dictionaryName = Console.ReadLine();

            if (!dictionaries.ContainsKey(dictionaryName))
            {
                Console.WriteLine("Словарь с таким названием не найден.");
                return;
            }

            Console.Write("Введите слово: ");
            string word = Console.ReadLine();

            Console.Write("Введите перевод слова: ");
            string translation = Console.ReadLine();

            if (!dictionaries[dictionaryName].ContainsKey(word))
            {
                dictionaries[dictionaryName].Add(word, new List<string>());
            }

            dictionaries[dictionaryName][word].Add(translation);

            Console.WriteLine($"Слово '{word}' и его перевод '{translation}' успешно добавлены в словарь '{dictionaryName}'.");
        }

        static void ReplaceWordTranslation(Dictionary<string, Dictionary<string, List<string>>> dictionaries)
        {
            Console.Write("Введите название словаря: ");
            string dictionaryName = Console.ReadLine();

            if (!dictionaries.ContainsKey(dictionaryName))
            {
                Console.WriteLine("Словарь с таким названием не найден.");
                return;
            }

            Console.Write("Введите слово, которое нужно заменить: ");
            string word = Console.ReadLine();

            if (!dictionaries[dictionaryName].ContainsKey(word))
            {
                Console.WriteLine("Слово не найдено в словаре.");
                return;
            }

            Console.Write("Введите новое слово: ");
            string newWord = Console.ReadLine();

            Console.Write("Введите новый перевод слова: ");
            string newTranslation = Console.ReadLine();

            dictionaries[dictionaryName].Remove(word);
            dictionaries[dictionaryName].Add(newWord, new List<string> { newTranslation });

            Console.WriteLine($"Слово '{word}' и его перевод успешно заменены на '{newWord}' и '{newTranslation}' в словаре '{dictionaryName}'.");
        }

        static void RemoveWordTranslation(Dictionary<string, Dictionary<string, List<string>>> dictionaries)
        {
            Console.Write("Введите название словаря: ");
            string dictionaryName = Console.ReadLine();

            if (!dictionaries.ContainsKey(dictionaryName))
            {
                Console.WriteLine("Словарь с таким названием не найден.");
                return;
            }

            Console.Write("Введите слово, которое нужно удалить: ");
            string word = Console.ReadLine();

            if (!dictionaries[dictionaryName].ContainsKey(word))
            {
                Console.WriteLine("Слово не найдено в словаре.");
                return;
            }

            dictionaries[dictionaryName].Remove(word);

            Console.WriteLine($"Слово '{word}' успешно удалено из словаря '{dictionaryName}'.");
        }

        static void SearchWordTranslation(Dictionary<string, Dictionary<string, List<string>>> dictionaries)
        {
            Console.Write("Введите название словаря: ");
            string dictionaryName = Console.ReadLine();

            if (!dictionaries.ContainsKey(dictionaryName))
            {
                Console.WriteLine("Словарь с таким названием не найден.");
                return;
            }

            Console.Write("Введите слово для поиска перевода: ");
            string word = Console.ReadLine();

            if (!dictionaries[dictionaryName].ContainsKey(word))
            {
                Console.WriteLine("Слово не найдено в словаре.");
                return;
            }

            Console.WriteLine($"Переводы слова '{word}':");

            foreach (string translation in dictionaries[dictionaryName][word])
            {
                Console.WriteLine(translation);
            }
        }

        static void ExportDictionary(Dictionary<string, Dictionary<string, List<string>>> dictionaries)
        {
            Console.Write("Введите название словаря: ");
            string dictionaryName = Console.ReadLine();

            if (!dictionaries.ContainsKey(dictionaryName))
            {
                Console.WriteLine("Словарь с таким названием не найден.");
                return;
            }

            Console.Write("Введите имя файла для экспорта: ");
            string fileName = Console.ReadLine();

            using (StreamWriter fileWriter = new StreamWriter(fileName))
            {
                foreach (var wordTranslations in dictionaries[dictionaryName])
                {
                    string word = wordTranslations.Key;
                    List<string> translations = wordTranslations.Value;

                    fileWriter.WriteLine($"Слово: {word}");

                    foreach (string translation in translations)
                    {
                        fileWriter.WriteLine($"Перевод: {translation}");
                    }

                    fileWriter.WriteLine();
                }
            }

            Console.WriteLine($"Словарь '{dictionaryName}' успешно экспортирован в файл '{fileName}'.");
        }
    }
}