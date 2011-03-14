using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using BibtexEntryManager.Models;
using BibtexEntryManager.Models.Collections;
using BibtexEntryManager.Models.Enums;

namespace BibtexEntryManager.Helpers
{
    /**Parser class is responsible for converting string data into map data structures fit for conversion into publication objects*/
    // Modified on 2010-10-06 by John Thow - Migration from Java to C#
    public static class Parser
    {

        /**Converts string data into maps suitable for constructing publication objects
         * @param data String containig publication data
         * @return List of maps where each map contains data for a publication object*/
        public static IEnumerable<Dictionary<string, string>> GetEntriesFrom(String data, out string errorString)
        {
            errorString = "";
            // List containing all data
            var allPubs = new List<Dictionary<String, String>>();


            // String to store entryType and citekey
            String entryType;

            // A queue for storing delimiters
            var stack = new Stack<String>();

            // Main stringbuilder
            var main = new StringBuilder(Regex.Replace(data, "\\s+", " "));

            // Opening delimiter
            var openingDelimiter = "";

            // Split the string at '@' sign indicating beginning of an entry so each array element has one entry
            var split = main.ToString().Split('@');

            // Iterate through the array
            foreach (var s in split)
            {
                try
                {
                    // New stringbuilder for one entry 
                    var str = new StringBuilder(s.Trim());

                    // Reset atName and atValue

                    // Reset entryEnd to false indicating start of new entry
                    bool entryEnd = false;

                    // If there is any data in stringbuilder
                    if (str.Length > 0)
                    {
                        // Delete everything after the last }
                        int index;
                        if ((index = LastIndexOf(str, '}')) + 1 < str.Length - 1)
                        {
                            // if it's -1, there is no final bracket?
                            if (index != -1)
                                str = SubString(str, 0, index);
                        }

                        // Map to store data
                        var table = new Dictionary<String, String>();

                        // Extract entry type
                        entryType = SubString(str, 0, IndexOf(str, '{')).ToString().Trim().ToLower();

                        // Create journals
                        if (entryType.Equals("string"))
                        {
                            // Delete the entry type
                            str.Remove(0, IndexOf(str, '{') + 1);

                            // Get the key
                            var key = SubString(str, 0, IndexOf(str, '=')).ToString().Trim();

                            // Get the value
                            var value = SubString(str, IndexOf(str, '=') + 1, IndexOf(str, '}')).ToString().Trim();

                            // Remove extra brackets and spaces
                            value = value.Substring(1, value.Length - 1).Trim();

                            // Add to journal collection
                            JournalCollection.addJournal(key, new Journal(key, value));

                            // Go to next entry
                            continue;
                        }

                        // Check if this is a valid entry type
                        Entry e;
                        if (!Enum.TryParse(entryType, true, out e))
                        {
                            // Continue to next iteration
                            continue;
                        }


                        // Put entrytype in map
                        table.Add(Field.Entrytype.ToString(), entryType);

                        // Delete entrytype from stringbuilder
                        str.Remove(0, IndexOf(str, '{') + 1);

                        // Extract citekey
                        var citekey = SubString(str, 0, IndexOf(str, ',')).ToString().Trim();

                        // Put citekey in map
                        table.Add(Field.Citekey.ToString().ToLower(), citekey);

                        // Delete citekey from stringbuilder
                        str.Remove(0, IndexOf(str, ',') + 1);

                        // Loop until end of entry
                        // This loop extracts attribute name and value)
                        for (var i = 0; i < str.Length; i++)
                        {
                            // Reset attribute name and value at each iteration

                            // Look for the first '=' sign 
                            if (str[i] == '=')
                            {
                                // Everything left of = sign is the attribute name extract it and put it in atName variable							
                                string atName = SubString(str, 0, i).ToString().Trim().ToLower();

                                // Delete attribute name
                                str.Remove(0, i + 1);

                                // Remove spaces at the front and back
                                str = new StringBuilder(str.ToString().Trim());

                                // Loop until end of entry but normally this loop will be broken out of before it reaches end of entry
                                // This loop extracts the value
                                int j;
                                for (j = 0; j < str.Length;)
                                {
                                    // Look for opening delimiters
                                    bool delimiter;
                                    bool inField;
                                    if (str[j] == '{')
                                    {
                                        // Set openingDelimiter
                                        openingDelimiter = "{";

                                        // If found put it on a queue
                                        stack.Push("{");

                                        // Delete everything until the opening delimiter (opening delimiter included)
                                        str.Remove(0, j + 1);

                                        // Set inField to true indicating field value is found 
                                        inField = true;

                                        // Set delimiter to true
                                        delimiter = true;
                                    }
                                    else if (str[j] == '(')
                                    {
                                        // Set openingDelimiter
                                        openingDelimiter = "(";

                                        // If found put it on a queue
                                        stack.Push("(");

                                        // Delete everything until the opening delimiter (opening delimiter included)
                                        str.Remove(0, j + 1);

                                        // Set inField to true indicating field value is found 
                                        inField = true;

                                        // Set delimiter to true
                                        delimiter = true;
                                    }
                                        // Look for opening delimiter
                                    else if (str[j] == '"')
                                    {
                                        // Set openingDelimiter
                                        openingDelimiter = "\"";

                                        // If found put it on a queue
                                        stack.Push("\"");

                                        // Delete everything until the opening delimiter (opening delimiter included)
                                        str.Remove(0, j + 1);

                                        // Set inField to true indicating field value is found
                                        inField = true;

                                        // Set delimiter to true
                                        delimiter = true;
                                    }
                                    else
                                    {
                                        inField = true;

                                        // Set delimiter to true
                                        delimiter = false;
                                    }

                                    // counter variable
                                    var k = 0;

                                    // if value is found start extracting
                                    while (inField)
                                    {
                                        // If delimeter was found
                                        if (delimiter)
                                        {
                                            // Check for delimeter
                                            if (str[k] == '}' && openingDelimiter.Equals("{"))
                                            {
                                                stack.Pop();
                                            }
                                                // Check for delimeter
                                            else if (str[k] == ')' && openingDelimiter.Equals("("))
                                            {
                                                stack.Pop();
                                            }
                                                // Check for delimeter
                                            else if (str[k] == '\"' && openingDelimiter.Equals("\""))
                                            {
                                                // In case of " as delimeter queue should popped in all the cases except the " are preceded by \ 
                                                if (k == 0)
                                                {
                                                    stack.Pop();
                                                }
                                                else if (str[k] == '\"' && str[k - 1] != '\\')
                                                {
                                                    stack.Pop();
                                                }
                                                else if (str[k] != '\"')
                                                {
                                                    stack.Pop();
                                                }
                                            }

                                            // Empty queue indicates value has come to an end
                                            if (stack.Count == 0)
                                            {
                                                // Set inField to false indicating value has finished
                                                inField = false;

                                                // Continue to next iteration
                                                continue;
                                            }



                                            // Look for other delimiters that match opening delimiter
                                            // This is required because some entries may have brackets within brackets
                                            if (str[k] == '{' && openingDelimiter.Equals("{"))
                                            {
                                                // Push them onto queue 
                                                stack.Push("{");
                                            }
                                            else if (str[k] == '(' && openingDelimiter.Equals("("))
                                            {
                                                // Push them onto queue 
                                                stack.Push("(");
                                            }
                                            else if (str[k] == '\"' && openingDelimiter.Equals("\""))
                                            {
                                                if (str.Length > k + 1)
                                                {
                                                    if (str[k + 1] == ',')
                                                    {
                                                        inField = false;
                                                    }
                                                }
                                                // Push them onto queue 
                                                stack.Push("\"");
                                            }
                                        }
                                            // No delimeter exists
                                        else
                                        {
                                            // look for characters that indicate end of field
                                            if (str[k] == ',' || str[k] == '}' || str[k] == ')')
                                            {
                                                inField = false;

                                                // Continue to next iteration
                                                continue;
                                            }
                                        }

                                        // Increment counter
                                        k++;
                                    }

                                    // Extract the value which is everything from 0'th character to the index indicated by k
                                    var atValue = SubString(str, 0, k).ToString().Trim();

                                    try
                                    {
                                        // Put attribute name and value in the map, if the value is not empty
                                        // should see a slight performance increase
                                        if (!string.IsNullOrEmpty(atValue))
                                            table.Add(atName, atValue);
                                    }
                                    catch (ArgumentException)
                                    {
                                        // assumption that if the old value's length is shorter than the new one,
                                        // then it has greater precedence.
                                        if (!table[atName].Equals(atValue) || table[atName].Length < atValue.Length)
                                        {
                                            table[atName] = atValue;
                                        }
                                    }
                                    // Delete the value from the stringbuilder
                                    str.Remove(0, k);

                                    // Look for equal to sign
                                    int equal = IndexOf(str, '=');

                                    // Clear the queue
                                    stack.Clear();

                                    // If there is no comma it means entry is finished 
                                    if (equal == -1)
                                    {
                                        // Set entry end to true indicating end of entry
                                        entryEnd = true;
                                    }
                                    else
                                    {
                                        // Look for next comma indicating the start of next entry
                                        int comma = IndexOf(str, ',');

                                        // Delete everything until the comma
                                        str.Remove(0, comma + 1);

                                        // Reset i to 0 to begin from the first character again
                                        i = 0;

                                        // Remove spaces at the front and back
                                        str = new StringBuilder(str.ToString().Trim());

                                        if (str.Length == 1 && (str[0] == '}' || str[0] == ')'))
                                        {
                                            entryEnd = true;
                                        }
                                    }

                                    // Break out of the while loop which extract values
                                    break;

                                }
                                // Check if the entry has ended
                                if (entryEnd)
                                {
                                    // Put the map in the list
                                    allPubs.Add(table);

                                    // Break out of the inner for loop which extract attribute names and values								
                                    break;
                                }
                            }
                        }
                    }

                }
                catch (IndexOutOfRangeException e)
                {
                    errorString = s + e.Message;
                    // only one entry per issue is not going to be lost, not all of them.
                    // quite a heavy-handed approach, could use some refinement
                }
            }
            return allPubs;
        }


        private static StringBuilder SubString(StringBuilder target, int startIndex, int endIndex)
        {
            if (startIndex == -1 || endIndex == -1)
                return target;

            return new StringBuilder(target.ToString().Substring(startIndex, endIndex));
        }


        private static int LastIndexOf(StringBuilder haystack, char needle)
        {
            for (int i = haystack.Length - 1; i >= 0; i--)
            {
                if (haystack[i] == needle)
                {
                    return i;
                }
            }
            return -1;

            //return haystack.ToString().LastIndexOf(needle);
        }


        private static int IndexOf(StringBuilder hs, char needle)
        {
            return hs.ToString().IndexOf(needle);
        }
    }

}