/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System.Collections.Generic;
using Dev2.Common.Interfaces.Data;
using Dev2.Data.Interfaces;

namespace Dev2.DataList.Contract
{
    public class RecordSetDefinition : IRecordSetDefinition {

        #region Attributes
        readonly string _setName;
        readonly IList<IDev2Definition> _columns;

        #endregion

        #region Ctor
        internal RecordSetDefinition(string setName, IList<IDev2Definition> columns) {
            _setName = setName;
            _columns = columns;
        }

        #endregion

        #region Properties
        public string SetName => _setName;

        public string XmlSetName => _setName.Replace("()", "");

        public IList<IDev2Definition> Columns => _columns;

        #endregion

    }
}
