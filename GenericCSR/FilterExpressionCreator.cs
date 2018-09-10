﻿using System;
using ExpressionBuilder.Common;
using ExpressionBuilder.Generics;
using ExpressionBuilder.Interfaces;
using ExpressionBuilder.Operations;
using Newtonsoft.Json.Linq;

namespace GenericCSR
{
    public class FilterExpressionCreator<TEntity, TPropertyMapper> where TEntity : class where TPropertyMapper:class,IPropertyMapper<TEntity>,new()
    {
        public Filter<TEntity> Create(JToken jsonFilters)
        {
            return ParseExpressionFromString(jsonFilters);
        }

        private Filter<TEntity> ParseExpressionFromString(JToken jsonFilters)
        {
            var groupConnector = GetGroupConnectorFromString(jsonFilters["groupOp"].ToString());
            var listOfFilters = (JArray)jsonFilters["rules"];
            return GetExpressionFromListOfStringFilters(listOfFilters, groupConnector);
        }

        private Filter<TEntity> GetExpressionFromListOfStringFilters(JArray listOfFilters, Connector groupConnector)
        {
            var filter = new Filter<TEntity>();
            foreach (var filterRule in listOfFilters)
            {
                var fullPropertyName = GetFullPropertyNameFromString(filterRule["field"].ToString());
                var propertyType = PropertyTypeExtractor.GetPropertyTypeAsString(typeof(TEntity),fullPropertyName);
                var op = filterRule["op"].ToString();
                IOperation operation = GetOperationByString(op);
                var dataStr = filterRule["data"].ToString();
                dynamic data = GetValidDataByOperationType(operation,dataStr,propertyType);
                filter.By(fullPropertyName, operation, data, groupConnector);
            }

            return filter;
        }

        private object GetValidDataByOperationType(IOperation operation,string data,string propertyType)
        {
            switch (propertyType)
            {

                case "Int32":
                    if (int.TryParse(data, out var intData))
                        return intData;
                    throw new Exception("Nije moguce izvriti konvertovanje u tip: " +propertyType);

                case "Nullable.Int32":
                    if (int.TryParse(data, out var nullableIntData))
                        return nullableIntData;
                    return null;

                case "Int64":
                    if (long.TryParse(data, out var longData))
                        return longData;
                    throw new Exception("Nije moguce izvriti konvertovanje u tip: " + propertyType);

                case "Nullable.Int64":
                    if (long.TryParse(data, out var nullableLongData))
                        return nullableLongData;
                    throw new Exception("Nije moguce izvriti konvertovanje u tip: " + propertyType);

                case "Double":
                    if (double.TryParse(data, out var doubleData))
                        return doubleData;
                    throw new Exception("Nije moguce izvriti konvertovanje u tip: " + propertyType);

                case "Nullable.Double":
                    if (double.TryParse(data, out var nullableDoubleData))
                        return nullableDoubleData;
                    return null;

                case "String":
                    return data;

                case "DateTime":
                    if (DateTime.TryParse(data, out var dateTimeData))
                        return dateTimeData;
                    throw new Exception("Nije moguce izvriti konvertovanje u tip: " + propertyType);

                case "Nullable.DateTime":
                    if (DateTime.TryParse(data, out var nullableDateTimeData))
                        return nullableDateTimeData;
                    return null;


                default:
                    throw new Exception("Nije podrzano kastovanje podatka za pretragu za propertyType : "+propertyType);
            }
        }

        private string GetFullPropertyNameFromString(string field)
        {
            var propertyMapper = new TPropertyMapper();
            var expresisonMember = propertyMapper.GetPropertyPathByName(field);
            return ExpressionPropertyExtractor.GetFullPropertyName(expresisonMember);
        }

        private Connector GetGroupConnectorFromString(string groupConnector)
        {
            return groupConnector == "AND" ? Connector.And : Connector.Or;
        }

        private IOperation GetOperationByString(string operation)
        {
            switch (operation)
            {
                case "eq":
                    return Operation.EqualTo;
                case "ne":
                    return Operation.NotEqualTo;
                case "lt":
                    return Operation.LessThan;
                case "le":
                    return Operation.LessThanOrEqualTo;
                case "gt":
                    return Operation.GreaterThan;
                case "ge":
                    return Operation.GreaterThanOrEqualTo;
                case "bw":
                    return Operation.StartsWith;
                case "ew":
                    return Operation.EndsWith;
                case "cn":
                    return Operation.Contains;
                case "nc":
                    return Operation.DoesNotContain;
                default:
                    throw new Exception("Nepostojeca operacija "+ operation +". Dozvoljene operacije su:eq,ne,lt,lt,gt,ge,bw,ew,cn,nc.");
            }
        }
    }
}