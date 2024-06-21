using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(1, 0, 0)]
    [Title("Sort List by Variable")]
    [Description(@"Sorts the List elements based on a variable found in a child gameobject. 
Each object in the list must have a child with the same name and a LocalNameVariables component.
Variable must have same name and be a number type. 

    ")]

    [Image(typeof(IconSort), ColorTheme.Type.Teal)]

    [Category("Variables/Sort List by Variable")]

    [Parameter("List Variable", "Local List or Global List which elements are sorted")]
    [Parameter("Order", "From Highest To Lowest puts the Highest elements to the Position first")]
    [Parameter("Name Of Child", "The name of the child gameobject where the LocalNameVariables component is")]
    [Parameter("Name Of Local Variable", "The name of the local variable to use for sorting, (variable must be a number type)")]
    [Keywords("Order", "Organize", "Array", "List", "Variables")]
    [Serializable]
    public class InstructionSortListByVariable : Instruction
    {
        private enum Order
        {
            LowestToHighest,
            HighestToLowest
        }

        [SerializeField] private CollectorListVariable m_ListVariable = new CollectorListVariable();
        [SerializeField] private Order m_Order = Order.HighestToLowest;
        [SerializeField] private string m_nameOfChild;
        [SerializeField] private string m_nameOfLocalVariable;

        public override string Title => $"Sort List by {m_nameOfLocalVariable} in {m_nameOfChild}";

        private Args m_Args;
        protected override Task Run(Args args)
        {
            List<object> elements = this.m_ListVariable.Get(args);

            this.m_Args = args;
            elements.Sort(this.SortingMethod);

            this.m_ListVariable.Fill(elements.ToArray(), args);
            return DefaultResult;
        }

        private int SortingMethod(object a, object b)
        {
            IdString type = this.m_ListVariable.GetTypeId(this.m_Args);


            if (type.Hash == ValueGameObject.TYPE_ID.Hash)
            {
                GameObject gameObjectA = a as GameObject;
                GameObject gameObjectB = b as GameObject;

                if (gameObjectA == null && gameObjectB == null) return 0;
                if (gameObjectA == null) return +1;
                if (gameObjectB == null) return -1;

                double numA = (double)gameObjectA.transform.Find(m_nameOfChild).GetComponent<LocalNameVariables>().Get(m_nameOfLocalVariable);
                double numB = (double)gameObjectB.transform.Find(m_nameOfChild).GetComponent<LocalNameVariables>().Get(m_nameOfLocalVariable);

                return this.m_Order == Order.LowestToHighest
                    ? numA.CompareTo(numB)
                    : numB.CompareTo(numA);
            }

            return 0;
        }
    }
}