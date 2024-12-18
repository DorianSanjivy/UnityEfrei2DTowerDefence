using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GridExtensions
{
    // Mélange les éléments d'un tableau 2D
    public static T[,] Shuffle<T>(this T[,] array)
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);
        List<T> flatList = new List<T>();

        // Convertir le tableau 2D en une liste plate
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                flatList.Add(array[x, y]);
            }
        }

        // Mélanger la liste
        flatList = flatList.OrderBy(item => UnityEngine.Random.value).ToList();

        // Remettre les éléments mélangés dans le tableau 2D
        int index = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                array[x, y] = flatList[index];
                index++;
            }
        }

        return array;
    }

    // Retourne un élément aléatoire du tableau 2D
    public static T PickRandom<T>(this T[,] array)
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);
        int randomX = UnityEngine.Random.Range(0, width);
        int randomY = UnityEngine.Random.Range(0, height);

        return array[randomX, randomY];
    }

    // Affiche le tableau 2D sous forme de chaîne de caractères
    public static string ToString2D<T>(this T[,] array)
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);
        string result = "";

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                result += array[y, x];
            }
            result += ", ";
        }

        return result;
    }

    // Affiche le tableau 2D dans le debug log
    public static void ToString2DDebugLog<T>(this T[,] array)
    {
        int rows = array.GetLength(0); // Nombre de lignes
        int cols = array.GetLength(1); // Nombre de colonnes

        for (int i = 0; i < rows; i++) // Parcourir les lignes
        {
            string row = "";
            for (int j = 0; j < cols; j++) // Parcourir les colonnes
            {
                row += array[i, j] + "\t";
            }
            Debug.Log(row);
        }

        return;
    }


    // Supprime l'élément à la position (x, y) en le remplaçant par la valeur par défaut
    public static void RemoveAt<T>(this T[,] array, int x, int y)
    {
        if (x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1))
        {
            array[x, y] = default(T);
        }
        else
        {
            Debug.LogWarning("Coordinates are out of bounds!");
        }
    }

    // Change l'élément à la position (x, y) avec un nouvel élément
    public static void ChangeAt<T>(this T[,] array, int x, int y, T newValue)
    {
        if (x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1))
        {
            array[x, y] = newValue;
        }
        else
        {
            Debug.LogWarning("Coordinates are out of bounds!");
        }
    }

    // Récupère l'élément à la position (x, y)
    public static T GetValueAt<T>(this T[,] array, int x, int y)
    {
        if (x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1))
        {
            return array[x, y];
        }
        else
        {
            Debug.LogWarning("Coordinates are out of bounds!");
            return default(T);
        }
    }

    // Redimensionne un tableau 2D (crée un nouveau tableau avec les nouvelles dimensions)
    public static T[,] Resize<T>(this T[,] array, int newWidth, int newHeight, T defaultValue = default(T))
    {
        T[,] newArray = new T[newWidth, newHeight];
        int minWidth = Math.Min(array.GetLength(0), newWidth);
        int minHeight = Math.Min(array.GetLength(1), newHeight);

        // Copier les éléments de l'ancien tableau dans le nouveau
        for (int x = 0; x < minWidth; x++)
        {
            for (int y = 0; y < minHeight; y++)
            {
                newArray[x, y] = array[x, y];
            }
        }

        // Remplir le reste avec la valeur par défaut
        for (int x = 0; x < newWidth; x++)
        {
            for (int y = 0; y < newHeight; y++)
            {
                if (x >= array.GetLength(0) || y >= array.GetLength(1))
                {
                    newArray[x, y] = defaultValue;
                }
            }
        }

        return newArray;
    }

    // Reset the 2D array, setting all elements to a specified default value
    public static T[,] ResetGrid<T>(this T[,] array, T defaultValue = default(T))
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                array[x, y] = defaultValue;
            }
        }
        return array;
    }

    // Remplit le tableau 2D avec une valeur spécifique un certain nombre de fois
    public static T[,] FillGrid<T>(this T[,] array, T fillValue, int fillCount, T emptyValue)
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);

        Debug.Log("-FillGrid: " + fillValue + " " + fillCount + " times");

        int filled = 0;
        while (filled < fillCount)
        {
            int randomX = UnityEngine.Random.Range(0, width);
            int randomY = UnityEngine.Random.Range(0, height);

            // Vérifie si la case est vide (contient la valeur emptyValue)
            if (EqualityComparer<T>.Default.Equals(array[randomX, randomY], emptyValue))
            {
                array[randomX, randomY] = fillValue;
                filled++;
            }
        }
        return array;
    }
}

